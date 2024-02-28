using Microsoft.EntityFrameworkCore;
using TestTask.Data.Mapping.File;
using TestTask.Data.Mapping.User;

namespace TestTask.Data.Mapping
{
    public static class ModelBuilderExtention
    {
        public static void AddMappers(this ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.ApplyConfiguration(new FileMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
            modelBuilder.ApplyConfiguration(new UserSessionMapper());
        }
    }
}
