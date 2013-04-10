using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qt.Domain.Entity;

namespace Qt.Web
{
    public class SessionManager
    {

        public static QtDbConnection CurrentQtDbConnection 
        {
            get { return (QtDbConnection)HttpContext.Current.Session["ActiveDb"]; } 
            set { HttpContext.Current.Session["ActiveDb"] = value; }
        }

        public static string CurrentWindowsUserName
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }

        public static List<KeyValuePair<string,string>> DbList
        {
            get { return (List<KeyValuePair<string,string>>)HttpContext.Current.Session["DbList"]; }
            set
            {
                HttpContext.Current.Session["DbList"] = new List<KeyValuePair<string, string>>();
                HttpContext.Current.Session["DbList"] = value;
            }
        }
    }
}