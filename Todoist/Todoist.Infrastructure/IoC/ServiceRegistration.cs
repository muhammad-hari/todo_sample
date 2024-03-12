using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todoist.Application.Contracts.Persistence;
using Todoist.Infrastructure.Persistence;
using Todoist.Infrastructure.Repositories;

namespace Todoist.Infrastructure.IoC
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // database connections
            services.AddDbContext<TaskDbContext>(options =>
                 options.UseSqlite(configuration.GetConnectionString("WriteDbConnection"),
                    sqliteOptions => {
                         sqliteOptions.MigrationsAssembly(typeof(TaskDbContext).Assembly.FullName);
                     }
                 ));

            services.AddDbContext<TasksReplicateDbContext>(options =>
                 options.UseSqlite(configuration.GetConnectionString("ReadDbConnection"),
                    sqliteOptions => {
                        sqliteOptions.MigrationsAssembly(typeof(TasksReplicateDbContext).Assembly.FullName);
                    }
                 ));

            // repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseReplicateRepository<>));
            services.AddScoped<ITasksRepository,TasksRepository>();
            services.AddScoped<ITasksReplicateRepository,TasksReplicateRepository>();

            // TODO: add notification services
            //services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            //services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
