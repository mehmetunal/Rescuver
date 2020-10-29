using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rescuer.EventBus.Abstractions;
using Rescuer.EventBus.Example.Publish.IntegrationEvents.Events;

namespace Rescuer.EventBus.Example.Publish.Controllers
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
        private readonly IEventBus _eventBus;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var _event = new MainChangedIntegrationEvent(1, 22, 23);
            _eventBus.Publish(_event);
            this._logger.LogInformation("OK");
            return Summaries.ToList();
        }
    }
}
