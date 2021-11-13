using System;
using System.Collections.Generic;

namespace ContactCoffee.Data.Messages
{
    public class SurveyResponseCreated
    {
        public string SurveyID { get; set; }
        public Dictionary<string, string> Answers { get; set; } = 
            new Dictionary<string, string>();

        public override string ToString() {
            var ret = "";
            ret += $"{this.SurveyID}:";
            foreach (KeyValuePair<string, string> ans in this.Answers)
            {
                ret += $"{ans.Key}={ans.Value};";
            }
            return ret;
        }
    }
}
