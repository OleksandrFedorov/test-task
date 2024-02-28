using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Entity;

namespace TestTask.Data.Mapping.User
{
    public class UserMapper : IEntityTypeConfiguration<Entity.User>
    {
        public void Configure(EntityTypeBuilder<Entity.User> builder)
        {
            builder.HasIndex(u => u.Name).IsUnique();

            builder
                .HasMany(u => u.Files)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(u => u.UserSession)
                .WithOne(us => us.User)
                .HasForeignKey<UserSession>(us => us.UserId);
        }
    }
}
