using Library.Common.Database.AppSettingsEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Core.PostgreSql.Contexts.Configurations
{
    class SettingsEntityConfiguration
        : IEntityTypeConfiguration<SettingEntity>
    {
        public void Configure(EntityTypeBuilder<SettingEntity> builder)
        {
            builder.HasKey(x => new { x.Name, x.Type });
            builder.Property(s => s.Value)
                .IsRequired(false);
        }
    }
}
