using ControllerSimulator.Exceptions;
using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class CustomerRepository : IRepository<Customer>
    {
        readonly CustomersContext _ctx;

        public CustomerRepository(CustomersContext dbContext)
        {
            _ctx = dbContext;
        }

        public Customer? Get(int id)
            => _ctx.Customers.Find(x => x.Id == id);

        public IEnumerable<Customer> GetAll()
            => _ctx.Customers;

        public void Update(Customer item)
        {
            var old = _ctx.Customers.Find(x => x.Id == item.Id);
            if (old != null)
            {
                int index = _ctx.Customers.IndexOf(old);
                _ctx.Customers[index] = item;
            }
            else
                throw new NoSuchElementException();
        }
    }
}
