namespace ContactCoffee.Web.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using ContactCoffee.Data.Messages;
    using ContactCoffee.Web.Context;
    using ContactCoffee.Web.Models;
    using ContactCoffee.Web.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class SurveyController : Controller
    {
        private readonly CoffeeContext context;
        private readonly ILogger<SurveyController> logger;
        private readonly IMessageService messageService;
        private IMapper mapper;

        public SurveyController(
            ILogger<SurveyController> logger,
            IMapper mapper,
            IMessageService messageService,
            CoffeeContext context)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.messageService = messageService;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody]SurveyResultsModel model)
        {
            this.context.SurveyResults.Add(model);
            this.context.SaveChanges();
            await this.messageService.SendMessage(this.mapper.Map<SurveyResponseCreated>(model));
            return this.Ok();
        }
    }
}