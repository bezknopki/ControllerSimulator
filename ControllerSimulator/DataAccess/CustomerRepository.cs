using ControllerSimulator.Exceptions;
using ControllerSimulator.Models;

namespace ControllerSimulator.DataAccess
{
    public class CustomerRepository : IRepository<Customer>
    {
        CustomersContext ctx;

        public CustomerRepository(CustomersContext dbContext)
        {
            ctx = dbContext;
        }

        public void Create(Customer item)
        {
            ctx.Customers.Add(item);
        }

        public void Delete(int id)
        {
            var item = ctx.Customers.Find(x => x.Id == id);
            if (item != null)
                ctx.Customers.Remove(item);
        }

        public Customer? Get(int id)
        {
            return ctx.Customers.Find(x => x.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return ctx.Customers;
        }

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
