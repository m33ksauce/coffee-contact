using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Azure.Messaging.ServiceBus;
using CoffeeContact.Data.Messages;
using Microsoft.Extensions.Logging;

namespace CoffeeContact.Web.Services
{
    public class MessageService : IMessageService
    {
        private ILogger<MessageService> _logger;
        private ServiceBusClient _client;
        private ServiceBusSender _sender;
        private static string key = "";
        private static string _connString = 
            "Endpoint=sb://coffeecontact-dev-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=" + key;
        private string _queueName = "defaultqueue";

        public MessageService(ILogger<MessageService> logger) {
            _logger = logger;
            _client = new ServiceBusClient(_connString);
            _sender = _client.CreateSender(_queueName);
        }

        public async Task SendMessage(Dictionary<string, string> responses) {
            var msg = new SurveyResponseCreated();
            msg.SurveyID = Guid.NewGuid().ToString();
            msg.Answers = responses;

            try
            {
                await _sender.SendMessageAsync(new ServiceBusMessage(msg.ToString()));
                _logger.LogInformation($"Sent message GUID {msg.SurveyID}");
            }
            finally
            {
                await _sender.DisposeAsync();
                await _client.DisposeAsync();
            }
        }
    }
}