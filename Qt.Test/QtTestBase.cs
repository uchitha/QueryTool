using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Qt.Data;
using Qt.Data.Common;
using Qt.Domain.Entity;
using Qt.Fixtures;

namespace Qt.Test
{
    [TestFixture]
    public class QtTestBase
    {

        private ITransactionManagerFactory _factory;
        protected QtDbConnection TargetQtDb;

        [SetUp]
        public virtual void SetUp()
        {
            TargetQtDb = new QtDbConnection()
                            {
                                ConnectionString =
                                    ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString,
                                Name = "Test Database",
                                Type = EnumDatabase.Oracle
                            };
            var testScenario = new BasicScenario();
            testScenario.RunAll();
        }

        [TearDown]
        public virtual void TearDown()
        {
            var testScenerio = new BasicScenario();
            testScenerio.Reset();
        }

        private ITransactionManagerFactory GetFactory()
        {
            if (_factory != null) return _factory;
            _factory = ServiceLocator.Current.GetInstance<ITransactionManagerFactory>();
            return _factory;
        }

        protected TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
        {
            using (var transManager = GetFactory().CreateManager())
            {
                return transManager.ExecuteCommand(command.Invoke);
            }
        }

        protected void ExecuteCommandWithoutResult(Action<IRepositoryLocator> command)
        {
            var func = new Func<IRepositoryLocator, bool>(locator =>
            {
                command.Invoke(locator);
                return true;
            });
            ExecuteCommand(func);
        }

        protected Query GetSimpleReadOnlyRpoQuery()
        {
            var queryRepository = new RepositoryInMemory<Query>();
            var newQuery = new Query()
            {
                DbType = EnumDatabase.Oracle,
                Description = "Qurey to fetch all planning changes",
                Name = "Planning Change",
                Text = "select * from rpo.stg_rpo_planning_change"
            };

            var q1 = queryRepository.Save(newQuery);

            var fetchedQuery = ExecuteCommand(locator => locator.GetById<Query>(q1.Id));

            Assert.IsNotNull(fetchedQuery);
            Assert.AreEqual(q1.Id,fetchedQuery.Id);

            return q1;
        }
    }
}
