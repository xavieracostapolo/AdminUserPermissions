using Elastic.Clients.Elasticsearch;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using Xacosta.AdminPermissions.Application.Feature;
using Xacosta.AdminPermissions.Application.Middlewares;
using Xacosta.AdminPermissions.Application.Services;
using Xacosta.AdminPermissions.Infraestructure;
using Xacosta.AdminPermissions.WebApi.Middleware;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"appsettings.{environment}.json",
        optional: true)
    .Build();

var builder = WebApplication.CreateBuilder(args);

Log.Information("Configurando Serilog.");
var clientElk = ConfigureLogging(configuration, environment);
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddDbContextPool<PersistenceContext>(o =>
{
    o.UseSqlServer(configuration.GetConnectionString("DataBase"));
});

Log.Information("Configurando MediaTR.");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(GetPermissionsQuery).Assembly,
                typeof(ModifyPermissionsCommand).Assembly,
                typeof(RequestPermissionsCommand).Assembly
                ));

Log.Information("Configurando Validators.");
//builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<RequestPermissionsCommand>, RequestPermissionsCommandValidator>();
builder.Services.AddScoped<IValidator<ModifyPermissionsCommand>, ModifyPermissionsCommandValidator>();

builder.Services.AddScoped<IPublisherBrokerService, PublisherBrokerService>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

Log.Information("Configurando IoC.");
builder.Services.AddDependencyInfraestructure(clientElk);

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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

var app = builder.Build();

app.UseExceptionHandler(_ => { });

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    PersistenceContext context = scope.ServiceProvider.GetRequiredService<PersistenceContext>();
    //context.Database.Migrate();
    context.Database.EnsureCreated();
}

app.Run();

ElasticsearchClient ConfigureLogging(IConfigurationRoot configuration, string environment)
{
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

public partial class Program { }