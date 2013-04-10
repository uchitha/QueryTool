

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Qt.Domain.Entity
{
    public class Query : IIdentityAware, IQuery
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public int ParameterCount { get; set; }
        public IList<QueryParameter> Parameters { get; private set; }

        public virtual User Owner { get; set; }

        public bool DisplayOnHomeScreen { get; set; }
        public bool Broken { get; set; }
        public bool Published { get; set; }
        public int DisplayOrder { get; set; }
        public bool WrapColumns { get; set; }

        public QueryGroup QueryGroup { get; set; }

        public EnumDatabase DbType { get; set; }

        public long Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Query()
        {
            Parameters = new List<QueryParameter>();
        }

        public void AssociateParameters(IList<QueryParameter> parameters)
        {
            foreach (var queryParameter in parameters)
            {
                AssociateParameter(queryParameter);
            }
        }

        private void AssociateParameter(QueryParameter parameter)
        {
            if (Parameters.Any(queryParameter => queryParameter.Name.ToUpperInvariant().Equals(parameter.Name.ToUpperInvariant())))
            {
                return;
            }
            Parameters.Add(parameter);
        }
    }
}