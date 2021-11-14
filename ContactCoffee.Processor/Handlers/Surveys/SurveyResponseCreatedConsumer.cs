namespace ContactCoffee.Processor.Handlers.Surveys
{
    using System;
    using System.Threading.Tasks;
    using ContactCoffee.Data.Messages;
    using MassTransit;
    using Microsoft.Extensions.Logging;

    public class SurveyResponseCreatedConsumer : IConsumer<SurveyResponseCreated>
    {
        private readonly ILogger<SurveyResponseCreatedConsumer> logger;

        private int runCount = 0;

        public SurveyResponseCreatedConsumer(ILogger<SurveyResponseCreatedConsumer> logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<SurveyResponseCreated> context)
        {
            this.logger.LogInformation($"{context.Message}");
            Console.WriteLine($"Received {++this.runCount} messages");

            return Task.CompletedTask;
        }
    }
}