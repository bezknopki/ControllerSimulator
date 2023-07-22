using ControllerSimulator.Models;
using System.Text;

namespace ControllerSimulator.Helpers
{
    public static class DumpHelper
    {
        public static readonly string _dumpPath = "..\\Dumps";

        public static string DumpPath => _dumpPath;

        public static void DumpCustomer(Customer customer)
        {
            var filePath = FilePathForCustomer(customer);
            var serializedCustomer = SerializationHelper.Serialize(customer);
            WriteToFile(serializedCustomer, filePath);
        }

        private static string FilePathForCustomer(Customer customer)
        {
            var fileName = "customer" + customer.Id + ".json";
            var filePath = Path.Combine(_dumpPath, fileName);
            return filePath;
        }

        public static void DumpCustomersFullName(IEnumerable<Customer> customers)
        {
            var filePath = FilePathForCustomersNames();
            var customersNames = BuildCustomersNamesString(customers);

            WriteToFile(customersNames, filePath);
        }

        private static string FilePathForCustomersNames()
        {
            var fileName = "customersNames.txt";
            var filePath = Path.Combine(_dumpPath, fileName);
            return filePath;
        }

        private static string BuildCustomersNamesString(IEnumerable<Customer> customers)
        {
            StringBuilder stringBuilder = new();
            foreach (var customer in customers)
                stringBuilder.AppendLine(customer.FullName);

            return stringBuilder.ToString();
        }

        private static void WriteToFile(string toWrite, string path)
        {
            using StreamWriter writer = new(path);
            writer.Write(toWrite);
        }
    }
}
