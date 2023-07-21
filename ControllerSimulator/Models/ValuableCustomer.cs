using ControllerSimulator.DataAccess;

namespace ControllerSimulator.Models
{
    public class ValuableCustomer : Customer
    {
        public ValuableCustomer(RoughCustomer roughCustomer)
            : base(roughCustomer) { }

        public override void UpdateQuota()
        {
            Quota += 2;
        }
    }
}
