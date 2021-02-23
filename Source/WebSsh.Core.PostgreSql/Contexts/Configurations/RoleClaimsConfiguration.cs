using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebSsh.Core.PostgreSql.Contexts.Configurations
{
    /// <summary>
    /// Entity configuration for <see cref="IdentityRoleClaim{TKey}"/>
    /// </summary>
    class RoleClaimsConfiguration
        : IEntityTypeConfiguration<IdentityRoleClaim<int>>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
        {
            builder.ToTable("role_claims");
        }
    }
}
