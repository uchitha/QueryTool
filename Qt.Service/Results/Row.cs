using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Qt.Service.Results
{
    public class Row
    {
        private List<string> _columnData;

        public Row()
        {
            _columnData = new List<string>();
        }

        public ReadOnlyCollection<string> ColumnData 
        {
            get { return _columnData.AsReadOnly(); }
        }

        public void AddValue(string value)
        {
            _columnData.Add(value);
        }
    }
}