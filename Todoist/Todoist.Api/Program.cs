using Todoist.Infrastructure.IoC;
using Todoist.Application.IoC;
using MassTransit;
using Todoist.Consumer;
using MassTransit.AspNetCoreIntegration;
using Todoist.Domain.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

// Register consumers and AutoMapper
builder.Services.AddScoped<CreateTaskConsumer>();
builder.Services.AddAutoMapper(typeof(Program));

// Configure MassTransit with RabbitMQ
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<CreateTaskConsumer>(); // Register consumers
    config.UsingRabbitMq((context, cfg) =>
    {
        var hostAddress = builder.Configuration["EventBusSettings:HostAddress"];
        cfg.Host(hostAddress); // Configure RabbitMQ host
        cfg.ReceiveEndpoint(EventBusConstants.CreateTaskQueue, e =>
        {
            e.ConfigureConsumer<CreateTaskConsumer>(context);
        });
        // Add health checks if necessary
        // cfg.UseHealthCheck(context);
    });
});

builder.Services.AddMassTransitHostedService(); // Add MassTransit hosted service

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
