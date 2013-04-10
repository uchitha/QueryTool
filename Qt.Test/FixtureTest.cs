using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Qt.Domain.Entity;
using Qt.Fixtures;

namespace Qt.Test
{
    [TestFixture]
    public class FixtureTest : QtTestBase
    {
        [Test]
        public void TestUserFixture()
        {
            var users = ExecuteCommand(locator => locator.FetchAll<User>()).ToList();
            Assert.IsNotEmpty(users);
        }
    }
}
