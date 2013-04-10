using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qt.Data;
using Qt.Domain.Entity;
using Qt.Service;
using Qt.Service.Results;

namespace Qt.Web.ControllerHelpers
{
    public class QueryExecuteHelper : QtBaseControllerHelper
    {
         private static QueryExecuteManager _executeService;

        static QueryExecuteHelper()
        {
            _executeService = new QueryExecuteManager();
        }

        public QueryResult ExecuteQueryAsText(long queryId)
        {
            var q = ExecuteCommand(locator => locator.GetById<Query>(queryId));
            if (q == null) throw new ApplicationException(string.Format("Query not found for id {0}", queryId));
            var dbConn = SessionManager.CurrentQtDbConnection;
            return _executeService.Execute(q, dbConn);
        }

        public QueryResult ExecuteQueryAsText(long queryId,List<QueryParameter> parameters)
        {
            var q = ExecuteCommand(locator => locator.GetById<Query>(queryId));
            if (q == null) throw new ApplicationException(string.Format("Query not found for id {0}", queryId));
            var dbConn = SessionManager.CurrentQtDbConnection;

            throw new NotImplementedException("Query execution with params not implemented");
        }
    }
}