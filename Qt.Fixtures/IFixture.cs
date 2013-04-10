using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qt.Data;

namespace Qt.Fixtures
{
    public interface IFixture
    {
        void Run(IRepositoryLocator locator);
    }
}
