using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class CustomersContext //:DbContext
    {
        public List<Customer> Customers { get; set; } = new();
    }
}
