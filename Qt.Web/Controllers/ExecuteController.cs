using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Qt.Data.Common;
using Qt.Domain.Entity;
using Qt.Web.ControllerHelpers;

namespace Qt.Web.Controllers
{
    public class ExecuteController : QtBaseController
    {
        //
        // GET: /Execute/
        public ActionResult RunQuery(long id, List<QueryParameter> parameters)
        {
            var executor = new QueryExecuteHelper();
            if (parameters != null && parameters.Count > 0)
            {
                var result = executor.ExecuteQueryAsText(id, parameters);
                return View("ViewResult", result);
            }
            else
            {
                var result = executor.ExecuteQueryAsText(id);
                return View("ViewResult", result);
            }
            
        }


    }
}
