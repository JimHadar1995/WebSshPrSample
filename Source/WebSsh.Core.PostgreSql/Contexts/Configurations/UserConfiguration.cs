using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSsh.Core.Entities;

namespace Identity.Core.PostgreSql.Contexts.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            builder.ToTable("users");
        }
    }
}
