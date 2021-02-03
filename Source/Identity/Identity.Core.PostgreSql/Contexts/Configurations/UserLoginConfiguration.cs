using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Core.PostgreSql.Contexts.Configurations
{
    class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<int>>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<IdentityUserLogin<int>> builder)
        {
            builder.ToTable("user_logins");
        }
    }
}
