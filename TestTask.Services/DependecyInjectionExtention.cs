using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Services.File;
using TestTask.Services.User;

namespace TestTask.Services
{
    public static class DependecyInjectionExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserService>();
            services.AddScoped<FileService>();
            return services;
        }
    }
}