using System;
using System.Collections.Generic;
using System.Data;
using Oracle.DataAccess.Client;
using Qt.Domain.Entity;
using Qt.Service.Results;

namespace Qt.Service
{
    class OracleExecutor : IQueryExecutor
    {
        public QueryResult ExecuteRead(QtDbConnection targetQtDb, Query query)
        {
            using (var cmd = GetCommand(targetQtDb,query))
            {
                try
                {
                    cmd.Connection.Open();
                    List<Field> fields;
                    var rows = new List<Row>();
                    using (var reader = cmd.ExecuteReader())
                    {
                        fields = GetQueryFieldInfo(reader.GetSchemaTable());
                        var fieldCount = reader.FieldCount;
                        while (reader.Read())
                        {
                            var row = new Row();
                            for (int i = 0; i < fieldCount; i++)
                            {
                                var obj = reader[i];
                                row.AddValue(obj.ToString());
                            }
                            rows.Add(row);
                        }
                    }

                   
                    return new QueryResult()
                               {
                                   Rows = rows,
                                   Fields = fields
                               };
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }

        }

        private List<Field> GetQueryFieldInfo(DataTable querySchema)
        {
            if (querySchema == null)
                throw new ApplicationException("Column meta data extraction failed");
            var fields = new List<Field>();
            foreach (DataRow row in querySchema.Rows)
            {
                //http://msdn.microsoft.com/en-us/library/system.data.oracleclient.oracledatareader.getschematable.aspx
                var field = new Field();
                field.Name = (string)row["ColumnName"];
                field.Type =  row["DataType"].GetType().FullName;
                fields.Add(field);
            }

            return fields;
        }

        private OracleCommand GetCommand(QtDbConnection targetQtDb, Query query)
        {
            return OracleCommandCreator.GetCommand(targetQtDb, query.Text);
        }
    }
}