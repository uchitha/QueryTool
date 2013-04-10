using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Qt.Domain.Entity;

namespace Qt.Service
{
    class OracleCommandCreator
    {
        public static OracleCommand GetCommand(QtDbConnection connection, string commandText)
        {
            var oracleConnection = GetConnection(connection);
            var oracleCommand = oracleConnection.CreateCommand();

            if (oracleCommand != null) { }
                oracleCommand.CommandText = commandText;

            oracleCommand.CommandType = CommandType.Text;
            return oracleCommand;
        }

        private static OracleConnection GetConnection(QtDbConnection connection)
        {
            return new OracleConnection(connection.ConnectionString);
        }


    }
}
