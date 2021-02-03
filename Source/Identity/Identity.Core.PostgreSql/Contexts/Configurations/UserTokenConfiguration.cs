using Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Core.PostgreSql.Contexts.Configurations
{
    class UserTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("user_tokens");
        }
    }
}
