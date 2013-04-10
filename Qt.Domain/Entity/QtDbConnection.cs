using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Qt.Domain.Entity
{
    public class QtDbConnection
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public EnumDatabase Type { get; set; }

        public static QtDbConnection TryGetConnectionFromString(string dbConnectionKey)
        {
            if (string.IsNullOrEmpty(dbConnectionKey))
            {
                return null;
            }

            var connection = ConfigurationManager.ConnectionStrings[dbConnectionKey];

            if (connection == null)
            {
                throw new ApplicationException(string.Format("Connection can not be found for {0}",dbConnectionKey));
            }

            var connString = ConfigurationManager.ConnectionStrings[dbConnectionKey].ConnectionString;
            var provider = ConfigurationManager.ConnectionStrings[dbConnectionKey].ProviderName;

            EnumDatabase type;
            if (provider.Contains("Oracle"))
            {
                type = EnumDatabase.Oracle;
            }
            else if (provider.Contains("MsSql"))
            {
                type = EnumDatabase.SqlServer;
            }
            else
            {
                throw new NotSupportedException("Datbase type is not supported");
            }


            var dbConnection = new QtDbConnection()
            {
                ConnectionString = connString,
                Name = dbConnectionKey,
                Type = type
            };

            return dbConnection;
        }
    }

}
