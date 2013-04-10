using System;
using System.IO;
using Qt.Data;
using Qt.Domain.Entity;
using System.Linq;

namespace Qt.Fixtures
{
    class QueryFixture : IFixture
    {
        public void Run(IRepositoryLocator locator)
        {
            CreateQueries(locator);
        }

        private static void CreateQueries(IRepositoryLocator locator)
        {
            var user = locator.FetchAll<User>().FirstOrDefault();
            if (user == null) throw new InvalidDataException("User is needed before creating query");
            var q1 = new Query
                         {
                             CreatedDateTime = DateTime.Now,
                             DbType = EnumDatabase.Oracle,
                             Description = "Test Query - User Permissions",
                             DisplayOnHomeScreen = true,
                             Name = "RPO - Permissions",
                             Text = "select p.description as User_Or_Group, r.description as RpoMS_Role from role r,principal p,role_principal rp where r.id = rp.role_id and p.id = rp.principal_id",
                             Owner = user,
                             ParameterCount = 0,
                             Published = true
                         };

            var q2 = new Query
                        {
                            CreatedDateTime = DateTime.Now,
                            DbType = EnumDatabase.Oracle,
                            Description = "Test Query - Rail Requirement Manager",
                            Text = "Select * from Query",
                            DisplayOnHomeScreen = true,
                            Name = "RPO - Rail Req",
                            Owner = user,
                            ParameterCount = 1,
                            Published = true
                        };

            var q3 = new Query
            {
                CreatedDateTime = DateTime.Now,
                DbType = EnumDatabase.Oracle,
                Description = "Test Query - PIM All sites",
                Text = "Select * from site",
                DisplayOnHomeScreen = true,
                Name = "PIM - sites",
                Owner = user,
                ParameterCount = 0,
                Published = true
            };
            locator.Save(q1);
            locator.Save(q2);
            locator.Save(q3);
        }
    }
}
