using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Qt.Domain.Entity;
using Qt.Web.ControllerHelpers;

namespace Qt.Web.Controllers
{
    public class HomeController : QtBaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Query");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";
            return View();
        }

        public ActionResult ChangeDb(string dbKey)
        {
            var qtDbConnection = QtDbConnection.TryGetConnectionFromString(dbKey);
            return RedirectToAction("Index", "Query",new {dbKey = qtDbConnection.Name}); //DbConnectionFilter will set the new default db
        }

    }
}
