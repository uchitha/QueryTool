using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Domain.Entity;
using Qt.Service.Results;

namespace Qt.Service
{
    public class QueryExecuteManager
    {
        private static Dictionary<EnumDatabase, IQueryExecutor> _executors;

        static QueryExecuteManager()
        {
            _executors = new Dictionary<EnumDatabase, IQueryExecutor>()
                             {
                                 {EnumDatabase.Oracle, new OracleExecutor()},
                                 {EnumDatabase.SqlServer, new MsSqlServerExecutor()},
                             };
        }

        public QueryResult Execute(Query q, QtDbConnection targetQtDb)
        {

            switch (q.DbType)
            {
                case EnumDatabase.Oracle:
                    {
                        return new OracleExecutor().ExecuteRead(targetQtDb,q);
                    }
                case EnumDatabase.SqlServer:
                    {
                        return new MsSqlServerExecutor().ExecuteRead(targetQtDb,q);
                    }
                default:
                    throw new NotImplementedException(string.Format("Database type {0} is not supported",q.DbType));
            }
        }
    }
}
