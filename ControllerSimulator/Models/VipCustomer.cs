using ControllerSimulator.DataAccess;
using System.Diagnostics;

namespace ControllerSimulator.Models
{
    public class VipCustomer : Customer
    {
        public VipCustomer(RoughCustomer roughCustomer)
            : base(roughCustomer) { }

        public override void UpdateQuota()
        {
            Quota += 5;
            Debug.WriteLine("QUOTA UPDATED");
        }
    }
}
