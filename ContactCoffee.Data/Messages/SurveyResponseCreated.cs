namespace ContactCoffee.Data.Messages
{
    using System;
    using System.Collections.Generic;

    public class SurveyResponseCreated
    {
        public string SurveyID { get; set; }

        public Dictionary<string, string> Answers { get; set; } =
            new Dictionary<string, string>();

        public override string ToString()
        {
            var ret = string.Empty;
            ret += $"{this.SurveyID}:";
            foreach (KeyValuePair<string, string> ans in this.Answers)
            {
                ret += $"{ans.Key}={ans.Value};";
            }

            return ret;
        }
    }
}
