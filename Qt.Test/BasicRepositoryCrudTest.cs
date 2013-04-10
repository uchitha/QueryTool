using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Qt.Data;
using Qt.Domain.Entity;


namespace Qt.Test
{
    [TestFixture]
    public class BasicRepositoryCrudTest : QtTestBase
    {
        [Test]
        public void TestRepositoryInMemoryDirectAccess()
        {
            var queryRepository = new RepositoryInMemory<Query>();
            var newQuery = new Query()
                               {
                                   DbType = EnumDatabase.Oracle,
                                   Description = "Qurey to fetch all planning changes",
                                   Name = "Planning Change",
                                   Text = "SELECT Id FROM DUAL"
                               };

            var newQuery2 = new Query()
            {
                DbType = EnumDatabase.Oracle,
                Description = "Qurey to fetch all rail req changes",
                Name = "Rail Reqs",
                Text = "SELECT Id FROM DUAL"
            };


            var q1 = queryRepository.Save(newQuery);
            var q2 = queryRepository.Save(newQuery2);

            Assert.IsNotNull(q1);
            Assert.AreNotEqual(0, q1.Id);
            Console.WriteLine("Query Id (1) " + q1.Id);

            Assert.IsNotNull(q2);
            Assert.AreNotEqual(q1.Id, q2.Id);

            Console.WriteLine("Query Id (2) " + q2.Id);
        }


        [Test]
        public void GetEntityTest()
        {
            ExecuteCommandWithoutResult(locator => CountEntities(locator, 2));
        }

        [Test]
        public void AddEntityTest()
        {
            var user = new User
                           {
                               Name = "Dennis Predovnik",
                               CreatedDateTime = DateTime.Now,
                           };
            var q1 = new Query
            {
                CreatedDateTime = DateTime.Now,
                DbType = EnumDatabase.Oracle,
                Description = "Query - Simple from Test",
                DisplayOnHomeScreen = true,
                Name = "RPO - Simple Query",
                Text = "SELECT * from Entity",
                Owner = user,
                ParameterCount = 0,
                Published = true
            };

            ExecuteCommandWithoutResult(locator =>
                               {
                                   locator.Save(user);
                                   locator.Save(q1);
                               });

            ExecuteCommandWithoutResult(locator => CountEntities(locator, 3));

        }

        [Test]
        public void UpdateEntityTest()
        {
            var user = ExecuteCommand(locator => locator.FetchAll<User>().FirstOrDefault());
            var name = user.Name;

            user.Name = name + "_updated";

            var updatedUser = ExecuteCommand(locator => locator.Update(user));
            var updatedName = updatedUser.Name;

            Assert.AreNotEqual(updatedName, name);
            Assert.IsTrue(updatedName.Contains("_updated"));
        }

        [Test]
        public void DeleteEntityTest()
        {
            ExecuteCommandWithoutResult(locator => CountEntities(locator, 2));
            var user = ExecuteCommand(locator => locator.FetchAll<User>().FirstOrDefault());
            ExecuteCommandWithoutResult(locator => locator.Delete(user));

            var userCount = ExecuteCommand(locator => locator.FetchAll<User>().Count());
            Assert.AreEqual(1, userCount);
        }

        private void CountEntities(IRepositoryLocator locator, int count)
        {
            var users = locator.FetchAll<User>().ToList();
            var queries = locator.FetchAll<Query>().ToList();

            Assert.AreEqual(count, users.Count);
            Assert.AreEqual(count, queries.Count);
        }

        private IEnumerable<Query> GetAllQueries(IRepositoryLocator locator, string desc)
        {
            var qr = locator.GetRepository<Query>();

            return qr.FetchAll().AsEnumerable();
        }
    }
}
