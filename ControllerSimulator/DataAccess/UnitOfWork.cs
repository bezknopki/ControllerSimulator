using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class UnitOfWork
    {
        private readonly IRepository<Customer> customerRepository;

        public UnitOfWork(CustomersContext ctx)
        {
            customerRepository = new CustomerRepository(ctx);
        }

        public IRepository<Customer> Customers
            => customerRepository;

        public IEnumerable<Customer> GetAllCustomers()
            => customerRepository.GetAll();

        public Customer? GetCustomer(int id)
            => customerRepository.Get(id);

        public IEnumerable<Customer> GetCustomersOlderThan(DateTime date)
            => customerRepository.GetAll()
            .Where(x => x.DateOfBirth < date);

        public int GetTotalQuota()
            => customerRepository.GetAll()
            .Sum(x => x.Quota);

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
