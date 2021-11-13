namespace CoffeeContact.Web.Services
{
    using System;
    using System.Threading.Tasks;
    using CoffeeContact.Data.Messages;
    using MassTransit;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class MessageService : IMessageService
    {
        private ILogger<MessageService> logger;
        private string connString;
        private string queueName = "defaultqueue";

        private ISendEndpointProvider provider;

        public MessageService(ILogger<MessageService> logger, IConfiguration config)
        {
            this.connString = config["ServiceBusConnectionString"];
            this.logger = logger;
            this.provider = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                cfg.Host(this.connString);
                cfg.Message<SurveyResponseCreated>(topo =>
                {
                    topo.SetEntityName(this.queueName);
                });
            });
        }

        public async Task SendMessage<T>(T message)
        where T : class
        {
            var sendEndpoint = await this.provider.GetSendEndpoint(
                new Uri("sb://coffeecontact-dev-servicebus.servicebus.windows.net/" + this.queueName));

            try
            {
                await sendEndpoint.Send<T>(message);
                this.logger.LogInformation($"Sent message GUID");
            }
            catch
            {
            }
        }
    }
}