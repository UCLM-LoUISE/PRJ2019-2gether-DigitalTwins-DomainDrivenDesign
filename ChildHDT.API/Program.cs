using ChildHDT.API.ApplicationServices;
using ChildHDT.Domain.DomainServices;
using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using ChildHDT.Infrastructure.IntegrationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.AspNetCore.Http.StatusCodes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new RoleJsonConverter());
        });

builder.Services.AddDbContext<ChildContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
builder.Services.AddScoped<RepositoryChild>();
builder.Services.AddScoped<INotificationHandler, NotificationHandler>();
builder.Services.AddScoped<IStressService, PWAStressService>();
builder.Services.AddHostedService<StressMonitoringService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
