using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeContact.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeContact.Web.Models;
using CoffeeContact.Data.Messages;

namespace CoffeeContact.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ILogger<SurveyController> _logger;
        private readonly IMessageService _messageService;
        private IMapper _mapper;

        public SurveyController(ILogger<SurveyController> logger, IMapper mapper, IMessageService messageService)
        {
            _logger = logger;
            _mapper = mapper;
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody]SurveyResultsModel model)
        {
            await _messageService.SendMessage(_mapper.Map<SurveyResponseCreated>(model));
            return Ok();
        }
    }
}