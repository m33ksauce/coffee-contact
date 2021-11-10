using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace CoffeeContact.Web.Models
{
    public class SurveyResultsModel
    {
        public string SurveyID { get; set; }

        [NotMapped]
        public Dictionary<string, string> Answers { get; set; } = 
            new Dictionary<string, string>();
    }
}