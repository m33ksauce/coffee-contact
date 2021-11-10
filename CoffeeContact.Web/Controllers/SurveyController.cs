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
using CoffeeContact.Web.Context;
using CoffeeContact.Data.Messages;

namespace CoffeeContact.Web.Controllers
{
    public class SurveyController : Controller
    {
        private readonly CoffeeContext _context;
        private readonly ILogger<SurveyController> _logger;
        private readonly IMessageService _messageService;
        private IMapper _mapper;

        public SurveyController(
            ILogger<SurveyController> logger,
            IMapper mapper,
            IMessageService messageService,
            CoffeeContext context)
        {
            _logger = logger;
            _mapper = mapper;
            _messageService = messageService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Submit([FromBody]SurveyResultsModel model)
        {
            _context.SurveyResults.Add(model);
            _context.SaveChanges();
            await _messageService.SendMessage(_mapper.Map<SurveyResponseCreated>(model));
            return Ok();
        }
    }
}