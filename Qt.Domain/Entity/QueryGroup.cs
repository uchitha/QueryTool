using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Qt.Domain.Entity
{
    public class QueryGroup : IIdentityAware
    {
        public QueryGroup()
        {
            Queries = new List<Query>();
        }

        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public IList<Query> Queries { get; set; }

        public long Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public void AddQueries(IList<Query> queries)
        {
            foreach (var q in queries)
            {
                Queries.Add(q);
            }
        }

        public void AddQuery(Query query)
        {
            Queries.Add(query);
        }
    }
}
