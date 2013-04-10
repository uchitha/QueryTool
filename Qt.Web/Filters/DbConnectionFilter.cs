using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Qt.Domain.Entity;

namespace Qt.Web.Filters
{
    public class DbConnectionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.RequestContext.HttpContext;

            var dbConnectionKey = httpContext.Request["DbKey"] ?? ConfigurationManager.AppSettings["DefaultDb"];

            var dbConnection = QtDbConnection.TryGetConnectionFromString(dbConnectionKey);

            SessionManager.CurrentQtDbConnection = dbConnection;

            base.OnActionExecuting(filterContext);
        }
    }
}