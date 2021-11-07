using System;
using System.Collections.Generic;

namespace CoffeeContact.Web.Models
{
    public class SurveyResultsModel
    {
        public string SurveyID { get; set; }
        public Dictionary<string, string> Answers { get; set; } = 
            new Dictionary<string, string>();
    }
}