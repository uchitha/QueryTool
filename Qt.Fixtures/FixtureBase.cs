using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Qt.Data;
using Qt.Data.Common;

namespace Qt.Fixtures
{
    public abstract class FixtureBase : ITestScenario
    {
        private readonly IList<IFixture> _fixtureList = new List<IFixture>();
        private ITransactionManagerFactory _factory;

        public void AddFixture(IFixture fixture)
        {
            _fixtureList.Add(fixture);
        }

        public void RunAll()
        {
            if (Fixtures.Count() == 0) throw new InvalidDataException("No Fixtures are added");
            ExecuteCommandWithoutResult(locator => Fixtures.ToList().ForEach(f => f.Run(locator)));
        }

        public void Reset()
        {
            var resetableLocator = ExecuteCommand(locator => locator as IResetable);
            if (resetableLocator == null) return;
            resetableLocator.Reset();
        }

        public IQueryable<IFixture> Fixtures
        {
            get { return _fixtureList.AsQueryable(); }
        }


        private ITransactionManagerFactory GetFactory()
        {
            if (_factory != null) return _factory;
            _factory = ServiceLocator.Current.GetInstance<ITransactionManagerFactory>();
            return _factory;
        }

        private TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command)
        {
            using (var transManager = GetFactory().CreateManager())
            {
                return transManager.ExecuteCommand(command.Invoke);
            }
        }

        private void ExecuteCommandWithoutResult(Action<IRepositoryLocator> command)
        {
            var func = new Func<IRepositoryLocator, bool>(locator =>
                                                              {
                                                                  command.Invoke(locator);
                                                                  return true;
                                                              });
            ExecuteCommand(func);
        }
    }
}
