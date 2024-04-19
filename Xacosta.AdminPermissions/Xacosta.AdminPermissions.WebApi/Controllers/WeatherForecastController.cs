using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Xacosta.AdminPermissions.Application.Events;
using Xacosta.AdminPermissions.Application.Feature.Permissions.Get;
using Xacosta.AdminPermissions.Domain.Models;

namespace Xacosta.AdminPermissions.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ISender sender,
            IPublisher publisher)
        {
            _logger = logger;
            _sender = sender;
            _publisher = publisher;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken)
        {
            var query = new GetPermissionsQuery()
            {
                Id = 1
            };

            _publisher.Publish(new ElasticSearchEvent()
            {
                Accion = "POST",
                Model = new Permission()
                {
                    ApellidoEmpleado = "Apellido",
                    FechaPermiso = DateOnly.FromDateTime(DateTime.Now),
                    NombreEmpleado = "Nombre de test",
                    Id = 2
                }
            }, cancellationToken); 

            _publisher.Publish(new KafkaEvent() { Id = Guid.NewGuid().ToString(), NameOperation = "modify" }, cancellationToken);
            
            var res = await _sender.Send(query);
            
            // Publish a notification that the order has been created
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
