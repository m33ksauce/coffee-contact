using System;
using System.Collections.Generic;

namespace CoffeeContact.Data.Messages
{
    public class SurveyResponseCreated
    {
        public String SurveyID { get; set; }
        public Dictionary<string, string> Answers { get; set; } = 
            new Dictionary<string, string>();
    }
}
