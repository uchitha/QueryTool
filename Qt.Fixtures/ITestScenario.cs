using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Data;

namespace Qt.Fixtures
{
    /// <summary>
    /// Provides a container to build fixtures in order
    /// </summary>
    interface ITestScenario
    {
        void AddFixture(IFixture fixture);
        void RunAll(); //Need to run within its own transaction created internal to method
        void Reset();  //Need to run within its own transaction created internal to method
        IQueryable<IFixture> Fixtures { get; }
    }
}
