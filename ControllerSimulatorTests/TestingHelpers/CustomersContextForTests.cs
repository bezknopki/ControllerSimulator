using ControllerSimulator.DataAccess;
using ControllerSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerSimulatorTests.TestingHelpers
{
    public class CustomersContextForTests : CustomersContext
    {
        public CustomersContextForTests() 
        {
            FillCustomersContextWithTestData();
        }

        private void FillCustomersContextWithTestData()
        {
            var testData = DataSourceSimulator.GetAllCustomers();
            Customers = MapCustomers(testData).ToList(); 
        }

        private IEnumerable<Customer> MapCustomers(IEnumerable<RoughCustomer> roughCustomers)
            => roughCustomers.Select(x => Customer.Factory.FromRawStruct(x));
    }
}
