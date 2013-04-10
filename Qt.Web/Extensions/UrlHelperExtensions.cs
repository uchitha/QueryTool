using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;

namespace System.Web.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string Image(this UrlHelper helper,string fileName)
        {
            return helper.Content("~/Images/{0}".FormatWith(fileName));
        }

        public static string QtImage(this UrlHelper helper, string fileName)
        {
            return helper.Content("~/Images/qt/{0}".FormatWith(fileName));
        }
    }
}