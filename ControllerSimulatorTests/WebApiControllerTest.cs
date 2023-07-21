using ControllerSimulator.Controllers;
using ControllerSimulator.DataAccess;
using ControllerSimulator.Helpers;
using ControllerSimulatorTests.TestingHelpers;

namespace ControllerSimulatorTests
{
    public class WebApiControllerTest
    {
        readonly WebApiControllerSimulator _testController;
        readonly CancellationTokenSource _cancellationTokenSource;
        readonly string _dumpPath;

        public WebApiControllerTest()
        {
            CustomersContext ctx = TestHelper.CreateContext();
            UnitOfWork uow = new(ctx);
            _testController = new(uow);

            _dumpPath = DumpHelper.DumpPath;

            _cancellationTokenSource = new();
        }

        [Fact]
        public async void GetAllCustomersTest()
        {
            var customers = await _testController.GetAllCustomers(_cancellationTokenSource.Token);
            Assert.True(customers.Count() == 4);
        }

        [Fact]
        public void FindCustomerTest()
        {
            var foundCustomer = _testController.FindCustomer(_cancellationTokenSource.Token, 1).Result;
            Assert.NotNull(foundCustomer);
            Assert.Equal(1, foundCustomer.Id);
        }

        [Fact]
        public void FindCustomerNotExistTest()
        {
            var foundCustomer = _testController.FindCustomer(_cancellationTokenSource.Token, 111).Result;
            Assert.Null(foundCustomer);
        }

        [Fact]
        public void FindOlderTest()
        {
            var date = new DateTime(1980, 5, 21);
            var foundCustomers = _testController.FindOlder(_cancellationTokenSource.Token, date).Result;
            Assert.NotNull(foundCustomers);

            foreach (var customer in foundCustomers)
                Assert.True(customer.DateOfBirth < date);
        }

        [Fact]
        public void GetTotalQuotaTest()
        {
            var expectedQuota = 27;
            var totalQuota = _testController.GetTotalQuota(_cancellationTokenSource.Token).Result;
            Assert.Equal(expectedQuota, totalQuota);
        }

        [Fact]
        public async void DumpCustomerTest()
        {
            string filePath = FilePathFor("customer1.json");

            await _testController.DumpCustomer(_cancellationTokenSource.Token, 1);

            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public async void DumpAllFullNamesTest()
        {
            string filePath = FilePathFor("customersNames.txt");

            await _testController.DumpAllFullNames(_cancellationTokenSource.Token);

            Assert.True(File.Exists(filePath));
        }

        private string FilePathFor(string fileName) 
            => Path.Combine(_dumpPath, fileName);

        [Fact]
        public void UpdateAllQuotasTest()
        {
            var customersQuotasAtStart = GetCustomersQuota();

            _testController.UpdateAllQuotasSync();

            AssertIfQuotaNotChanged(customersQuotasAtStart);
        }

        private Dictionary<int, int> GetCustomersQuota()
        {
            Dictionary<int, int> customersQuota = new();
            foreach (var customer in _testController.GetAllCustomers(_cancellationTokenSource.Token).Result)
                customersQuota.Add(customer.Id, customer.Quota);
            return customersQuota;
        }

        private void AssertIfQuotaNotChanged(Dictionary<int, int> customersQuota)
        {
            var customers = _testController.GetAllCustomers(_cancellationTokenSource.Token).Result;
            foreach (var customer in customers)
                Assert.NotEqual(customersQuota[customer.Id], customer.Quota);
        }
    }
}