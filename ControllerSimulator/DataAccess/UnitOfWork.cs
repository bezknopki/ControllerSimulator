using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class UnitOfWork
    {
        private CustomerRepository customerRepository;
        private CustomersContext customersContext;

        public UnitOfWork(CustomersContext ctx)
        {
            customersContext = ctx;
            customerRepository = new(customersContext);
        }

        public CustomerRepository Customers
            => customerRepository ??= new(customersContext);

        public IEnumerable<Customer> GetAllCustomers()
            => customerRepository.GetAll();

        public Customer? GetCustomer(int id)
            => customerRepository.Get(id);

        public IEnumerable<Customer> GetCustomersOlderThan(DateTime date)
            => customerRepository.GetAll().Where(x => x.DateOfBirth < date);

        public int GetTotalQuota()
            => customerRepository.GetAll().Sum(x => x.Quota);

        public IEnumerable<Customer> UpdateAllQuotas()
        {
            foreach (var customer in customerRepository.GetAll())
            {
                customer.UpdateQuota();
                yield return customer;
            }
        }
    }
}
