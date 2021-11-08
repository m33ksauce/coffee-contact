using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Azure.Messaging.ServiceBus;
using CoffeeContact.Data.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace CoffeeContact.Web.Services
{
    public class MessageService : IMessageService
    {
        private ILogger<MessageService> _logger;
        private string _connString;
        private string _queueName = "defaultqueue";

        private ISendEndpointProvider _provider;

        public MessageService(ILogger<MessageService> logger, IConfiguration config) {
            _connString = config["ServiceBusConnectionString"] + config["ServiceBusKey"];
            _logger = logger;
            _provider = Bus.Factory.CreateUsingAzureServiceBus(cfg => {
                cfg.Host(_connString);
                cfg.Message<SurveyResponseCreated>(topo => {
                    topo.SetEntityName(_queueName);
                });
            });
        }

        public async Task SendMessage<T>(T message)
        where T : class
        {
            var sendEndpoint = await _provider.GetSendEndpoint(
                new Uri("sb://coffeecontact-dev-servicebus.servicebus.windows.net/" + _queueName));

            try
            {
                await sendEndpoint.Send<T>(message);
                _logger.LogInformation($"Sent message GUID");
            }
            catch {}
        }
    }
}