using Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Core.PostgreSql.Contexts.Configurations
{
    class UserHashHistoryConfiguration
    : IEntityTypeConfiguration<UserPasswordHashHistory>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<UserPasswordHashHistory> builder)
        {
            builder.HasOne(_ => _.User)
                .WithMany(_ => _.HashHistory)
                .HasForeignKey(_ => _.UserId)
                .HasConstraintName("fk_user_hash_history_id_user_id");

            builder.ToTable("user_hash_histories");
        }
    }
}
