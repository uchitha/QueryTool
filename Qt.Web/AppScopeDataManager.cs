using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qt.Data;
using Qt.Domain;
using Qt.Domain.Entity;

namespace Qt.Web
{
    public class AppScopeDataManager : QtDataManagerBase
    {

        static AppScopeDataManager()
        {
            var groups = ExecuteCommand(locator => locator.FetchAll<QueryGroup>().ToList());
            InsertToAppScope("Groups",groups,DateTime.UtcNow.AddHours(1));
        }

        public static List<QueryGroup> QueryGroups
        {
            get
            {
                var groups = (List<QueryGroup>) HttpRuntime.Cache.Get("Groups");

                if (groups == null)
                {
                    groups = ExecuteCommand(locator => locator.FetchAll<QueryGroup>().ToList());
                    InsertToAppScope("Groups", groups, DateTime.UtcNow.AddHours(1));
                }
                            
                return groups;
            }
            set
            {
                InsertToAppScope("Groups",value,DateTime.UtcNow.AddHours(1));
            }
        }

        private static void InsertToAppScope(string key, object obj,DateTime expiration)
        {
            HttpRuntime.Cache.Insert(key, obj, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

    }
}