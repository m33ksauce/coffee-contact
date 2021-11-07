using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Azure.Messaging.ServiceBus;
using CoffeeContact.Data.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CoffeeContact.Web.Services
{
    public class MessageService : IMessageService
    {
        private ILogger<MessageService> _logger;
        private static string key = "";
        private static string _connString = 
            "Endpoint=sb://coffeecontact-dev-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=" + key;
        private string _queueName = "defaultqueue";

        private ISendEndpointProvider _provider;

        public MessageService(ILogger<MessageService> logger) {
            _logger = logger;
            _provider = Bus.Factory.CreateUsingAzureServiceBus(cfg => {
                cfg.Host(_connString);
                cfg.Message<SurveyResponseCreated>(topo => {
                    topo.SetEntityName(_queueName);
                });
            });
        }

        public async Task SendMessage(Dictionary<string, string> responses) {
            var sendEndpoint = await _provider.GetSendEndpoint(
                new Uri("sb://coffeecontact-dev-servicebus.servicebus.windows.net/" + _queueName));

            var msg = new SurveyResponseCreated();
            msg.SurveyID = Guid.NewGuid().ToString();
            msg.Answers = responses;

            try
            {
                
                await sendEndpoint.Send<SurveyResponseCreated>(msg);
                _logger.LogInformation($"Sent message GUID {msg.SurveyID}");
            }
            catch {}
        }
    }
}