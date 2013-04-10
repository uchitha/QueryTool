using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Qt.Domain.Entity;

namespace Qt.Data
{
    public class QtDevInitializer : DropCreateDatabaseAlways<QtContext>
    {
        protected override void Seed(QtContext context)
        {

            var user = new User() { Name = "Chris Ranasinghe", CreatedDateTime = DateTime.Now };
            context.Users.Add(user);

            var group = new QueryGroup() { Name = "RPO Group", CreatedDateTime = DateTime.Now };
            context.Groups.Add(group);

            var query = new Query() { Description = "Planning Change Tracker", CreatedDateTime = DateTime.Now, Name = "Planing Change", Text = "SELECT * FROM ME", Owner = user, QueryGroup = group };
            context.Queries.Add(query);

            var queryRh = new Query() { Description = "Schedule Tracker", CreatedDateTime = DateTime.Now, Name = "Schedules", Text = "SELECT * FROM SCHEDULES", Owner = user };
            context.Queries.Add(queryRh);

            base.Seed(context);
        }
    }
}
