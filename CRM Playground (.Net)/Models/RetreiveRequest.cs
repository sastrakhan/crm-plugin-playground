using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Playground.Models
{
    public class RetreiveRequest
    {
        public string EntityName { get; set; }
        public string[] FieldsWanted { get; set; }
        public List<FieldMatch> FieldQuery { get; set; }
    }

    public class FieldMatch
    {
        public string FieldtoMatch { get; set; }
        public string FielldValue { get; set; }
    }
}
