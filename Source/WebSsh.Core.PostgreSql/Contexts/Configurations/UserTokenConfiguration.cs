using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebSsh.Core.Entities;

namespace WebSsh.Core.PostgreSql.Contexts.Configurations
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
