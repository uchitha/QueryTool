using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qt.Web.Filters
{
    public class ApplicationDataFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (SessionManager.DbList == null)
            {
                var connections = ConfigurationManager.ConnectionStrings;
                var dbList =
                    (from ConnectionStringSettings connection in connections
                     select new KeyValuePair<string, string>(connection.Name, connection.Name)).ToList();

                SessionManager.DbList = dbList;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}