using ControllerSimulator.Controllers;
using ControllerSimulator.DataAccess;
using ControllerSimulator.Helpers;
using ControllerSimulatorTests.TestingHelpers;

namespace ControllerSimulatorTests
{
    public class WebApiControllerTest
    {
        readonly WebApiControllerSimulator testController;
        readonly CancellationTokenSource cancellationTokenSource;
        readonly string dumpPath;

        public WebApiControllerTest()
        {
            CustomersContext ctx = TestHelper.CreateContext();
            UnitOfWork uow = new(ctx);
            testController = new(uow);

            dumpPath = DumpHelper.DumpPath;

            cancellationTokenSource = new();
        }

        [Fact]
        public async void GetAllCustomersTest()
        {
            var customers = await testController.GetAllCustomers(cancellationTokenSource.Token);
            Assert.True(customers.Count() == 4);
        }

        [Fact]
        public void FindCustomerTest()
        {
            var foundCustomer = testController.FindCustomer(cancellationTokenSource.Token, 1).Result;
            Assert.NotNull(foundCustomer);
            Assert.Equal(1, foundCustomer.Id);
        }

        [Fact]
        public void FindCustomerNotExistTest()
        {
            var foundCustomer = testController.FindCustomer(cancellationTokenSource.Token, 111).Result;
            Assert.Null(foundCustomer);
        }

        [Fact]
        public void FindOlderTest()
        {
            var date = new DateTime(1980, 5, 21);
            var foundCustomers = testController.FindOlder(cancellationTokenSource.Token, date).Result;
            Assert.NotNull(foundCustomers);

            foreach (var customer in foundCustomers)
                Assert.True(customer.DateOfBirth < date);
        }

        [Fact]
        public void GetTotalQuotaTest()
        {
            var expectedQuota = 27;
            var totalQuota = testController.GetTotalQuota(cancellationTokenSource.Token).Result;
            Assert.Equal(expectedQuota, totalQuota);
        }

        [Fact]
        public async void DumpCustomerTest()
        {
            string filePath = FilePathFor("customer1.json");

            await testController.DumpCustomer(cancellationTokenSource.Token, 1);

            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public async void DumpAllFullNamesTest()
        {
            string filePath = FilePathFor("customersNames.txt");

            await testController.DumpAllFullNames(cancellationTokenSource.Token);

            Assert.True(File.Exists(filePath));
        }

        private string FilePathFor(string fileName) 
            => Path.Combine(dumpPath, fileName);

        [Fact]
        public void UpdateAllQuotasTest()
        {
            var customersQuotasAtStart = GetCustomersQuota();

            testController.UpdateAllQuotasSync();

            AssertIfQuotaNotChanged(customersQuotasAtStart);
        }

        private Dictionary<int, int> GetCustomersQuota()
        {
            Dictionary<int, int> customersQuota = new();
            foreach (var customer in testController.GetAllCustomers(cancellationTokenSource.Token).Result)
                customersQuota.Add(customer.Id, customer.Quota);
            return customersQuota;
        }

        private void AssertIfQuotaNotChanged(Dictionary<int, int> customersQuota)
        {
            var customers = testController.GetAllCustomers(cancellationTokenSource.Token).Result;
            foreach (var customer in customers)
                Assert.NotEqual(customersQuota[customer.Id], customer.Quota);
        }
    }
}