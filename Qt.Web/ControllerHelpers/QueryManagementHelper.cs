using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Qt.Core;
using Qt.Domain.Entity;
using Qt.Service;

namespace Qt.Web.ControllerHelpers
{
    public class QueryManagementHelper : QtBaseControllerHelper
    {
        private static QueryManagmentService _queryService;

        static QueryManagementHelper()
        {
            _queryService = new QueryManagmentService();
        }

        public void SetCurrentDb(string dbKey)
        {
            var connString = ConfigurationManager.ConnectionStrings[dbKey].ConnectionString;

        }

        public IEnumerable<Query> GetQueryList()
        {
            return ExecuteCommand(locator => _queryService.GetQueryList(locator));
        }

        public IEnumerable<Query> GetQueryListForGroup(int groupId)
        {
            return ExecuteCommand(locator => _queryService.GetQueryListForGroup(locator,groupId));
        }

        public Query GetQuery(long queryId)
        {
            return ExecuteCommand(locator => locator.GetById<Query>(queryId));
        }

        public bool DeleteQuery(long queryId)
        {
            return ExecuteCommand(locator => _queryService.DeleteQuery(locator, queryId));
        }

        public Query UpdateQuery(Query updatedQuery)
        {
            return ExecuteCommand(locator => _queryService.UpdateQuery(locator, updatedQuery));
        }

        public Query CreateQuery(Query newQuery)
        {
            var currentUser = SessionManager.CurrentWindowsUserName;
            return ExecuteCommand(locator => _queryService.AddQuery(locator, newQuery, currentUser));
        }

        public QueryGroup GetQueryGroup(long queryId)
        {
            return ExecuteCommand(locator => locator.GetById<QueryGroup>(queryId));
        }

        public List<QueryGroup> GetAllGroups()
        {
            return ExecuteCommand(locator => locator.FetchAll<QueryGroup>().ToList());
        }

        public Query AddParameters(long queryId, List<QueryParameter> parameters )
        {
            return ExecuteCommand(locator => _queryService.AddParameters(locator, queryId, parameters));
        }
    }
}