using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Xacosta.AdminPermissions.Application.Feature.Permissions.Get;
using Xacosta.AdminPermissions.Application.Middlewares;
using Xacosta.AdminPermissions.Infraestructure;
using Xacosta.AdminPermissions.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

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
