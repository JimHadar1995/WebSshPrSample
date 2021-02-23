using System.Reflection;
using System.Text.RegularExpressions;
using Library.Common.Database.AppSettingsEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Npgsql.NameTranslation;
using WebSsh.Core.Entities;

namespace WebSsh.Core.PostgreSql.Contexts
{
    /// <summary>
    /// Db context
    /// </summary>
    public sealed class WebSshDbContext : IdentityDbContext<
        User,
        Role,
        int,
        IdentityUserClaim<int>,
        UserRole,
        IdentityUserLogin<int>,
        IdentityRoleClaim<int>,
        RefreshToken>
    {
        private static readonly Regex KeysRegex = new Regex("^(PK|FK|IX)_", RegexOptions.Compiled);
        private readonly IConfiguration _config;

        /// <inheritdoc/>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public WebSshDbContext(DbContextOptions<WebSshDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        /// <inheritdoc />
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseNpgsql(_config.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies();
            }

            optionsBuilder.EnableSensitiveDataLogging(false);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(WebSshDbContext)));

            FixSnakeCaseNames(modelBuilder);
        }

        #region [ Db sets ]

        /// <summary>
        /// Настройки
        /// </summary>
        public DbSet<SettingEntity> Settings { get; private set; }

        /// <summary>
        /// История паролей
        /// </summary>
        public DbSet<UserPasswordHashHistory> UserPaswordHistories { get; private set; }

        /// <summary>
        /// Refresh tokens
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<LogEntity> Logs { get; private set; }

        #endregion

        #region [ Help methods ]

        private void FixSnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            foreach (var table in modelBuilder.Model.GetEntityTypes())
            {
                ConvertToSnake(mapper, table);
                foreach (var property in table.GetProperties())
                {
                    ConvertToSnake(mapper, property, table);
                }

                foreach (var primaryKey in table.GetKeys())
                {
                    ConvertToSnake(mapper, primaryKey);
                }

                foreach (var foreignKey in table.GetForeignKeys())
                {
                    ConvertToSnake(mapper, foreignKey);
                }

                foreach (var indexKey in table.GetIndexes())
                {
                    ConvertToSnake(mapper, indexKey);
                }
            }
        }

        private void ConvertToSnake(INpgsqlNameTranslator mapper, object mutableProp, IMutableEntityType? entity = null)
        {
            switch (mutableProp)
            {
                case IMutableEntityType table:

                    table.SetTableName(ConvertGeneralToSnake(mapper, table.GetTableName()));
                    if (table.GetTableName().StartsWith("asp_net_"))
                    {
                        table.SetTableName(table.GetTableName().Replace("asp_net_", string.Empty));
                    }

                    break;
                case IMutableProperty property:
                    {
                        if (entity != null)
                        {
                            property.SetColumnName(ConvertGeneralToSnake(mapper,
                                property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName(), entity.GetSchema()))));
                        }
                    }

                    break;
                case IMutableKey primaryKey:
                    primaryKey.SetName(ConvertKeyToSnake(mapper, primaryKey.GetName()));
                    break;
                case IMutableForeignKey foreignKey:
                    foreignKey.SetConstraintName(ConvertKeyToSnake(mapper, foreignKey.GetConstraintName()));
                    break;
                case IMutableIndex indexKey:
                    indexKey.SetDatabaseName(ConvertKeyToSnake(mapper, indexKey.GetDatabaseName()));
                    break;
            }
        }

        private string ConvertKeyToSnake(INpgsqlNameTranslator mapper, string keyName) =>
            ConvertGeneralToSnake(mapper, KeysRegex.Replace(keyName, match => match.Value.ToLower()));

        private string ConvertGeneralToSnake(INpgsqlNameTranslator mapper, string entityName) =>
            mapper.TranslateMemberName(ModifyNameBeforeConvertion(entityName));

        private string ModifyNameBeforeConvertion(string entityName) => entityName;

        #endregion
    }
}
