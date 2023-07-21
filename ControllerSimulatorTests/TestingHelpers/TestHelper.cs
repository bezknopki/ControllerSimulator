using ControllerSimulator.DataAccess;

namespace ControllerSimulatorTests.TestingHelpers
{
    public static class TestHelper
    {
        public static CustomersContext CreateContext() => new CustomersContextForTests();
    }
}
