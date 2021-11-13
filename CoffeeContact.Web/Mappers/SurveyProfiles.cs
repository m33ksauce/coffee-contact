namespace CoffeeContact.Web.Mappers
{
    using AutoMapper;
    using CoffeeContact.Data.Messages;
    using CoffeeContact.Web.Models;

    public class SurveyProfiles : Profile
    {
        public SurveyProfiles()
        {
            this.CreateMap<SurveyResultsModel, SurveyResponseCreated>();
        }
    }
}