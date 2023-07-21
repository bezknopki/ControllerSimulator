using ControllerSimulator.Controllers;
using ControllerSimulator.DataAccess;
using ControllerSimulator.Helpers;
using ControllerSimulatorTests.TestingHelpers;
using System.Configuration;

namespace ControllerSimulatorTests
{
    public class WebApiControllerTest
    {
        readonly WebApiControllerSimulator testController;
        readonly CancellationToken cancellationToken;
        string dumpPath;
        public WebApiControllerTest()
        {
            CustomersContext ctx = TestHelper.CreateContext();
            UnitOfWork uow = new(ctx);
            dumpPath = DumpHelper.DumpPath;
            testController = new(uow);
            cancellationToken = new();
        }

        [Fact]
        public async void GetAllCustomersTest()
        {
            var customers = await testController.GetAllCustomers(cancellationToken);
            Assert.True(customers.Count() == 4);
        }

        [Fact]
        public void FindCustomerTest()
        {
            var foundCustomer = testController.FindCustomer(cancellationToken, 1).Result;
            Assert.NotNull(foundCustomer);
            Assert.Equal(1, foundCustomer.Id);
        }

        [Fact]
        public void FindCustomerNotExistTest()
        {
            var foundCustomer = testController.FindCustomer(cancellationToken, 111).Result;
            Assert.Null(foundCustomer);
        }

        [Fact]
        public void FindOlderTest()
        {
            var date = new DateTime(1980, 5, 21);
            var foundCustomers = testController.FindOlder(cancellationToken, date).Result;
            Assert.NotNull(foundCustomers);

            foreach (var customer in foundCustomers)
                Assert.True(customer.DateOfBirth < date);
        }

        [Fact]
        public void GetTotalQuotaTest()
        {
            var expectedQuota = 27;
            var totalQuota = testController.GetTotalQuota(cancellationToken).Result;
            Assert.Equal(expectedQuota, totalQuota);
        }

        [Fact]
        public async void DumpCustomerTest()
        {
            string fileName = "customer1.json";
            string filePath = Path.Combine(dumpPath, fileName);

            await testController.DumpCustomer(cancellationToken, 1);

            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public async void DumpAllFullNamesTest()
        {
            string fileName = "customersNames.txt";
            string filePath = Path.Combine(dumpPath, fileName);

            await testController.DumpAllFullNames(cancellationToken);

            Assert.True(File.Exists(filePath));
        }

        [Fact]
        public async void UpdateAllQuotasTest()
        {
            await testController.UpdateAllQuotas(cancellationToken);
        }
    }
}