using ControllerSimulator.DataAccess;

namespace ControllerSimulator.Models
{
    public class RegularCustomer : Customer
    {
        public RegularCustomer(RoughCustomer roughCustomer)
            : base(roughCustomer) { }

        public override void UpdateQuota()
        {
            Quota++;
        }
    }
}
