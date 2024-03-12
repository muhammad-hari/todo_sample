using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Todoist.Application.Contracts.Persistence;

namespace Todoist.Application.IoC
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            //services.AddMassTransit(config => {
            //    config.UsingRabbitMq((context, cfg) => {
            //        cfg.Host(configuration["EventBusSettings:HostAddress"]);
            //        // Add health checks if necessary
            //        // cfg.UseHealthCheck(context);
            //    });
            //});
            //services.AddMassTransitHostedService();

            return services;
        }
    }
}
