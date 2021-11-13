namespace ContactCoffee.Web.Mappers
{
    using AutoMapper;
    using ContactCoffee.Data.Messages;
    using ContactCoffee.Web.Models;

    public class SurveyProfiles : Profile
    {
        public SurveyProfiles()
        {
            this.CreateMap<SurveyResultsModel, SurveyResponseCreated>();
        }
    }
}