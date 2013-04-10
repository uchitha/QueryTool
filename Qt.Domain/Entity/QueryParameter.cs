using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qt.Domain.Entity
{
    public class QueryParameter
    {
        public Query Query { get; set; }

        public int ParameterNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultValue { get; set; }
    }
}
