using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestTask.Data.Entity;

namespace TestTask.Data.Mapping.User
{
    public class UserSessionMapper : IEntityTypeConfiguration<UserSession>
    { 
        public void Configure(EntityTypeBuilder<UserSession> builder)
        {
            /*builder
                .HasOne(us => us.User)
                .WithOne(u => u.UserSession)
                .HasForeignKey<UserSession>(us => us.Id);*/
        }
    }
}
