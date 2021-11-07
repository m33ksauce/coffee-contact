using System;
using System.Threading.Tasks;
using MassTransit;
using CoffeeContact.Data.Messages;
using Microsoft.Extensions.Logging;

namespace CoffeeContact.Processor.Handlers.Surveys
{
    public class SurveyResponseCreatedConsumer : IConsumer<SurveyResponseCreated>
    {
        private readonly ILogger<SurveyResponseCreatedConsumer> _logger;

        public SurveyResponseCreatedConsumer(ILogger<SurveyResponseCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<SurveyResponseCreated> context)
        {
            _logger.LogInformation($"{context.Message}");
            Console.WriteLine("it worked");

            return Task.CompletedTask;
        }
    }
}