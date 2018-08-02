
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Linq;
using System.Net;
using Microsoft.Xrm.Tooling.Connector;
using CRM.Playground.Models;
using System.Collections.Generic;

namespace CFF.SM.ProcessWorkers.EntityFramework.Repositories
{
    public class CRMRepository
    {

        // Connect to the CRM web service using a connection string.
        private IOrganizationService _serviceProxy;

        public CRMRepository(string crmUrl)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            CrmServiceClient conn = new CrmServiceClient(crmUrl);

            _serviceProxy = (IOrganizationService)conn.OrganizationWebProxyClient != null ?
                             (IOrganizationService)conn.OrganizationWebProxyClient :
                             (IOrganizationService)conn.OrganizationServiceProxy;
        }

        public EntityCollection GetRecords(RetreiveRequest request)
        {
            var fieldsToMatch = request.FieldQuery.Select(fq => fq.FieldtoMatch).ToArray();
            var fieldsValue = request.FieldQuery.Select(fq => fq.FielldValue).ToArray();

            QueryByAttribute querybyattribute = new QueryByAttribute(request.EntityName);
            querybyattribute.ColumnSet = createColumnSet(request.FieldsWanted);

            querybyattribute.Attributes.AddRange(fieldsToMatch);
            querybyattribute.Values.AddRange(fieldsValue);

            EntityCollection retrieved = _serviceProxy.RetrieveMultiple(querybyattribute);

            return retrieved;
        }

        private ColumnSet createColumnSet(string[] columns)
        {
            var columnSet = new ColumnSet();
            for(int i = 0; i < columns.Length; i++){
                columnSet.AddColumn(columns[i]);
            }

            return columnSet;
        }

    }
}