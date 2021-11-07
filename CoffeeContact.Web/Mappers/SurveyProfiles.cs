using System;
using AutoMapper;
using CoffeeContact.Data.Messages;
using CoffeeContact.Web.Models;

namespace CoffeeContact.Web.Mappers
{
    public class SurveyMapperProfiles : Profile
    {
        public SurveyMapperProfiles() {
            CreateMap<SurveyResultsModel, SurveyResponseCreated>();
        }
    }
}