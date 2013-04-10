using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Domain.Entity;
using Qt.Service.Results;

namespace Qt.Service
{
    public interface IQueryExecutor
    {
        QueryResult ExecuteRead(QtDbConnection targetQtDb, Query query);
    }
}
