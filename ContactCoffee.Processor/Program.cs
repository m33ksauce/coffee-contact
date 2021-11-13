using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactCoffee.Data.Messages;
using ContactCoffee.Processor.Handlers.Surveys;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactCoffee.Processor
{
    public class Program
    {
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
                            var connString = hostContext.Configuration["ServiceBusConnectionString"] +
                                hostContext.Configuration["ServiceBusKey"];

                            cfg.Host(connString);
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
