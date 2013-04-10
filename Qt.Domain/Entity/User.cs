using System;
using System.Collections.Generic;

namespace Qt.Domain.Entity
{
    public class User : IIdentityAware
    {
        public User()
        {
            Queries = new List<Query>();
        }

        public string Name { get; set; }
        public string Password { get; set; }
        public IList<Query> Queries { get; set; }

        public long Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
