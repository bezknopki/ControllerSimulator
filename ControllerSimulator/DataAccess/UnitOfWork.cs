using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class UnitOfWork
    {
        private readonly IRepository<Customer> _customerRepository;

        public UnitOfWork(CustomersContext ctx)
        {
            _customerRepository = new CustomerRepository(ctx);
        }

        public IRepository<Customer> Customers
            => _customerRepository;

        public IEnumerable<Customer> GetAllCustomers()
            => _customerRepository.GetAll();

        public Customer? GetCustomer(int id)
            => _customerRepository.Get(id);

        public IEnumerable<Customer> GetCustomersOlderThan(DateTime date)
            => _customerRepository.GetAll()
            .Where(x => x.DateOfBirth < date);

        public int GetTotalQuota()
            => _customerRepository.GetAll()
            .Sum(x => x.Quota);

        public IEnumerable<Customer> UpdateAllQuotas()
        {
            foreach (var customer in _customerRepository.GetAll())
                customer.UpdateQuota();

            return _customerRepository.GetAll();
        }
    }
}
