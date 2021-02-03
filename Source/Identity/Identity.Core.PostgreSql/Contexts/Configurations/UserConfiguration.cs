using Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
