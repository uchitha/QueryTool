﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;
using Qt.Core;
using Qt.Data;
using Qt.Data.Common;
using Qt.Fixtures;
using Qt.Web.Filters;

namespace Qt.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DbConnectionFilter());
            filters.Add(new ApplicationDataFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            InitializeDi();
            InitializeData(); //Only for InMemory


            BundleTable.Bundles.RegisterTemplateBundles();
        }

        /// <summary>
        /// Initialize Unity
        /// </summary>
        private void InitializeDi()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["UnityConfigFile"];
            var bootStrapper = new UnityBootStrapper() { ConfigurationFileName = path};
            bootStrapper.Initialize();
        }

        private void InitializeData()
        {
            var testScenario = new BasicScenario();
            testScenario.RunAll();
        }

      
    }
}