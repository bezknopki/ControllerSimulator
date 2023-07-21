using ControllerSimulator.DataAccess;
using ControllerSimulator.Exceptions;

namespace ControllerSimulator.Models
{
    public class CustomerFactory
    {
        public Customer FromRawStruct(RoughCustomer roughCustomer)
        {
            return roughCustomer.rank switch
            {
                0 => new RegularCustomer(roughCustomer),
                1 => new ValuableCustomer(roughCustomer),
                2 => new VipCustomer(roughCustomer),
                _ => throw new NotSupportedRankException(),
            };
        }
    }
}
