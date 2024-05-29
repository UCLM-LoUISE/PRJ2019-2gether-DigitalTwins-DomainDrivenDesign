using ChildHDT.Infrastructure.InfrastructureServices;
using ChildHDT.Infrastructure.InfrastructureServices.Context;
using ChildHDT.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.AspNetCore.Http.StatusCodes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new RoleJsonConverter());
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddDbContext<ChildContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();
builder.Services.AddScoped<RepositoryChild>();

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = Status307TemporaryRedirect;
//    options.HttpsPort = 5001;
//});

var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Infrastructure"));
var settingsPath = Path.Combine(basePath, "appsettings.json");
builder.Configuration.AddJsonFile(settingsPath, optional: true, reloadOnChange: true);

var postgreSqlSettings = builder.Configuration.GetSection("PostgreSQLSettings").Get<PostgreSQLSettings>();

// Add DbContext with connection string from configuration
//builder.Services.AddDbContext<ChildContext>(options =>
//    options.UseNpgsql(postgreSqlSettings.ConnectionString));


//builder.Services.Configure<MQTTSettings>(builder.Configuration.GetSection("MQTTSettings"));
//builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));
//builder.Services.Configure<PostgreSQLSettings>(builder.Configuration.GetSection("PostgreSQLSettings"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
