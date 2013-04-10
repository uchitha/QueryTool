using System.Collections.Generic;

namespace Qt.Service.Results
{
    public class QueryResult
    {
        public List<Row> Rows { get; set; }
        public List<Field> Fields { get; set; }

        public bool Validate()
        {
            if (Rows.Count == 0) return true;
            var columnCount = Rows[0].ColumnData.Count;
            var fieldCount = Fields.Count;
            //Dataset should have equal number of colums matching the fields
            return columnCount == fieldCount;

        }
    }
}