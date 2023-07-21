using ControllerSimulator.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerSimulatorTests.TestingHelpers
{
    public static class TestHelper
    {
        public static CustomersContext CreateContext() => new CustomersContextForTests();
    }
}
