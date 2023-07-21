using ControllerSimulator.Models;
using Newtonsoft.Json;
using System.Text;

namespace ControllerSimulator.Helpers
{
    public static class DumpHelper
    {
        public static readonly string dumpPath = "..\\Dumps";

        public static string DumpPath => dumpPath;       

        public static void DumpCustomer(Customer customer)
        {
            string fileName = "customer" + customer.Id + ".json";
            string filePath = Path.Combine(dumpPath, fileName);
            var serializedCustomer = JsonConvert.SerializeObject(customer);
            WriteToFile(serializedCustomer, filePath);
        }

        public static void DumpCustomersFullName(IEnumerable<Customer> customers)
        {
            string fileName = "customersNames.txt";
            string filePath = Path.Combine(dumpPath, fileName);

            StringBuilder stringBuilder = new();
            foreach (var customer in customers)
                stringBuilder.AppendLine(customer.FullName);

            WriteToFile(stringBuilder.ToString(), filePath);
        }

        private static void WriteToFile(string toWrite, string path)
        {
            using StreamWriter writer = new(path);
            writer.Write(toWrite);
        }
    }
}
