using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Data;
using Qt.Domain.Entity;

namespace Qt.Fixtures
{
    class QueryGroupFixture : IFixture
    {
        public void Run(IRepositoryLocator locator)
        {
            CreateGroups(locator);
        }

        private void CreateGroups(IRepositoryLocator locator)
        {
            var q1List = locator.FetchAll<Query>().OrderBy(q => q.Id).Take(2).ToList();
            var q2List = locator.FetchAll<Query>().OrderByDescending(q => q.Id).Take(2).ToList();

            var q1Group = new QueryGroup()
                              {
                                  Name = "Group 1",
                                  CreatedDateTime = DateTime.Now,
                              };
            q1Group.AddQueries(q1List);
            locator.Save(q1Group);

            var q2Group = new QueryGroup()
            {
                Name = "Group 2",
                CreatedDateTime = DateTime.Now,
            };
            q2Group.AddQueries(q2List);

            locator.Save(q2Group);
        }
    }
}
