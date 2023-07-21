using ControllerSimulator.Exceptions;
using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class CustomerRepository : IRepository<Customer>
    {
        readonly CustomersContext ctx;

        public CustomerRepository(CustomersContext dbContext)
        {
            ctx = dbContext;
        }

        public Customer? Get(int id)
            => ctx.Customers.Find(x => x.Id == id);

        public IEnumerable<Customer> GetAll()
            => ctx.Customers;

        public void Update(Customer item)
        {
            var old = ctx.Customers.Find(x => x.Id == item.Id);
            if (old != null)
            {
                int index = ctx.Customers.IndexOf(old);
                ctx.Customers[index] = item;
            }
            else
                throw new NoSuchElementException();
        }
    }
}
