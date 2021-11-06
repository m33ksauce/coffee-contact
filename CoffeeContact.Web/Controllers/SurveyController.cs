using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoffeeContact.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeContact.Web.Models;

namespace CoffeeContact.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly IMessageService _messageService;

        public SurveyController(ILogger<SurveyController> logger, IMessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody]Dictionary<string, string> surveyResponses)
        {
            await _messageService.SendMessage(surveyResponses);
            return Ok();
        }
    }
}