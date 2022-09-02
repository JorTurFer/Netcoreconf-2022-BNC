using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Netcoreconf.Controllers
{
    public class ServiceBusController : ControllerBase
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusOptions _options;
        public ServiceBusAdministrationClient _adminClient;

        public ServiceBusController(ServiceBusClient client, ServiceBusAdministrationClient adminClient, IOptions<ServiceBusOptions> options)
        {
            _options = options.Value;
            _client = client;
            _adminClient = adminClient;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetMessageCount()
        {
            var properties = await _adminClient.GetQueueRuntimePropertiesAsync(_options.Queue);
            return Content(properties.Value.TotalMessageCount.ToString());
        }

        [HttpGet("add/{message}")]
        public async Task<IActionResult> AddMessage(string message)
        {
            var sender = _client.CreateSender(_options.Queue);
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            var sbMessage = new ServiceBusMessage(message);
            messageBatch.TryAddMessage(sbMessage);
            await sender.SendMessagesAsync(messageBatch);
            return Ok();
        }
    }
}
