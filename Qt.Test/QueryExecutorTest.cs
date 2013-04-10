using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Qt.Service;

namespace Qt.Test
{
    [TestFixture]
    public class QueryExecutorTest : QtTestBase
    {
        [Test]
        public void TestOracleReadQuery()
        {
            var q = GetSimpleReadOnlyRpoQuery();
            var result = new QueryExecuteManager().Execute(q, TargetQtDb);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Validate());
        }
    }
}
