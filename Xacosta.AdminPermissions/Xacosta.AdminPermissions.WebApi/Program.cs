using Elastic.Clients.Elasticsearch;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using Xacosta.AdminPermissions.Application.Feature.Permissions.Get;
using Xacosta.AdminPermissions.Application.Middlewares;
using Xacosta.AdminPermissions.Domain.ContractsInfraestructure;
using Xacosta.AdminPermissions.Infraestructure;
using Xacosta.AdminPermissions.Infraestructure.Services;
using Xacosta.AdminPermissions.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Configurando Serilog.");
var clientElk = ConfigureLogging();
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddDbContextPool<PersistenceContext>(o =>
{
    //o.UseSqlServer("Specify the database connection string here...");
    o.UseInMemoryDatabase("dbTest");
});

Log.Information("Configurando MediaTR.");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(GetPermissionsQuery).Assembly
                ));

builder.Services.AddSingleton<IElasticService>(new ElasticService(clientElk));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

Log.Information("Configurando IoC.");
builder.Services.AddDependencyInfraestructure();

Log.Information("Configurando Automapper.");
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

Log.Information("Configurando FluentValidation.");
//builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(_ => { });

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

ElasticsearchClient ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

    var settings = new ElasticsearchClientSettings(new Uri(configuration["ElasticConfiguration:Uri"]))
        .DefaultIndex("text-text")
        .EnableDebugMode()
        .PrettyJson()
        .RequestTimeout(TimeSpan.FromMinutes(2));

    return new ElasticsearchClient(settings);
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow.Ticks}"
    };
}