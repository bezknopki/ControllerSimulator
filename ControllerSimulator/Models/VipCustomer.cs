using ControllerSimulator.DataAccess;

namespace ControllerSimulator.Models
{
    public class VipCustomer : Customer
    {
        public VipCustomer(RoughCustomer roughCustomer)
            : base(roughCustomer) { }

        public override void UpdateQuota()
        {
            Quota += 5;
        }
    }
}
