using CFF.SM.ProcessWorkers.EntityFramework.Repositories;
using CRM.Playground.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Playground__.Net_
{
    class Program
    {
        static void Main(string[] args)
        {
            //CRM credentials
            var password = "";
            var userName = "";
            var crmURL = "";

            var url = $"Url=https://{crmURL}.api.crm.dynamics.com/api/data/v9.0/; Username={userName}; Password={password}; authtype=Office365";

            CRMRepository _crm = new CRMRepository(url);
            RetreiveRequest request = new RetreiveRequest
            {  //Fetch a System user with the email of "johnny@gmail.com"
                EntityName = "systemuser",
                FieldsWanted = new string[] { "emailaddress1", "fullname", "statecode" },
                FieldQuery = new List<FieldMatch>
                {
                    new FieldMatch{FieldtoMatch = "emailaddress1", FielldValue = "johnny@gmail.com"}
                }
            };

            var getMatchingUsers = _crm.GetRecords(request);

            //Get their status of Active or Inactive
            var firstContactsStatus= ((OptionSetValue)getMatchingUsers.Entities.FirstOrDefault()["statecode"]).Value;
            var secondContactsemail = getMatchingUsers.Entities.Skip(1).FirstOrDefault()["emailaddress1"];       
        }
    }
}
