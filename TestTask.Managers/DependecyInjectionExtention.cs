using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Data.Repository;
using TestTask.Managers.File;
using TestTask.Services.File;

namespace TestTask.Managers
{
    public static class DependecyInjectionExtention
    {
        public static void AddManagers(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Sqlite");

            services.AddSingleton(c => new FileManagerConfiguration(configuration["FileManager:DefaultLocation"]));
            services.AddScoped(c => new TestTaskRepository(connection));
            services.AddScoped<FileManager>();
        }
    }
}
