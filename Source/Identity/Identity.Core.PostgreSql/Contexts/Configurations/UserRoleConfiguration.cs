using Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Core.PostgreSql.Contexts.Configurations
{
    class UserRoleConfiguration
        : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.Roles)
                .HasForeignKey(_ => _.UserId);

            builder.HasOne(_ => _.Role)
                .WithMany(_ => _.Users)
                .HasForeignKey(_ => _.RoleId);
        }
    }
}
