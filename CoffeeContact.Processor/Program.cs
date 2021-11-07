using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeContact.Data.Messages;
using CoffeeContact.Processor.Handlers.Surveys;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoffeeContact.Processor
{
    public class Program
    {
        private static string key = "";
        private static string _connString = 
            "Endpoint=sb://coffeecontact-dev-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=" + key;
        private static string _queueName = "defaultqueue";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<SurveyResponseCreatedConsumer>();

                        x.UsingAzureServiceBus((context,cfg) =>
                        {
                            cfg.Host(_connString);
                            cfg.ReceiveEndpoint(_queueName, e => {
                                e.SelectBasicTier();
                                e.ConfigureConsumeTopology = false;
                                e.ConfigureConsumer<SurveyResponseCreatedConsumer>(context);
                            });
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddMassTransitHostedService(true);
                });
    }
}
