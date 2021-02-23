using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSsh.Core.Entities;

namespace WebSsh.Core.PostgreSql.Contexts.Configurations
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.ToTable("roles");
        }
    }
}
