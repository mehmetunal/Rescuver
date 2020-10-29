using Rescuer.EventBus.Abstractions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rescuer.EventBus.Example.Subscribe.IntegrationEvents.Events;
using System;

namespace Rescuer.EventBus.Example.Subscribe.IntegrationEvents.EventHandling
{
    public class MainChangedIntegrationEventHandler : IIntegrationEventHandler<MainChangedIntegrationEvent>
    {
        private readonly ILogger<MainChangedIntegrationEventHandler> _logger;
        public MainChangedIntegrationEventHandler(ILogger<MainChangedIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task Handle(MainChangedIntegrationEvent @event)
        {
            _logger.LogInformation("IntegrationEventContext", $"{@event.Id}-{Program.AppName}");
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            ///DataBase İşlemi

            _logger.LogInformation($"Accepted evet:{@event}");
            return Task.CompletedTask;
        }
    }
}
