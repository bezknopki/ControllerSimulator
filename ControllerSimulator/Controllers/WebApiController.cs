using ControllerSimulator.DataAccess;
using ControllerSimulator.Helpers;
using ControllerSimulator.Models;
using System.Configuration;

namespace ControllerSimulator.Controllers
{
    /// <summary>
    /// Our simulated Web API controller.
    /// </summary>
    public class WebApiControllerSimulator
    {
        readonly UnitOfWork _unitOfWork;

        public WebApiControllerSimulator(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Returns all customers or nothing in case of any error.</returns>
        public async Task<IEnumerable<Customer>> GetAllCustomers(CancellationToken ct)
        {
            return await Task.Run(
                () =>
                _unitOfWork.GetAllCustomers()
                , ct);
        }

        /// <summary>
        /// Finds specific customer.
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="iCustomerId"></param>
        /// <returns>Returns found customer or null.</returns>
        public async Task<Customer?> FindCustomer(CancellationToken ct, int iCustomerId)
        {
            return await Task.Run(
                () =>
                _unitOfWork.GetCustomer(iCustomerId)
                , ct);
        }

        /// <summary>
        /// Finds all customers older than the specified date.
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="dt">The specified date.</param>
        /// <returns>Returns all customers older than the specified date or nothing in case of any error.</returns>
        public async Task<IEnumerable<Customer>> FindOlder(CancellationToken ct, DateTime dt)
        {
            return await Task.Run(
                () =>
                _unitOfWork.GetCustomersOlderThan(dt)
                , ct);
        }

        /// <summary>
        /// Produces total quota (sum) of all customers.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>Total quota or -1 in case of any error.</returns>
        public async Task<int> GetTotalQuota(CancellationToken ct)
        {
            return await Task.Run(
                () =>
                _unitOfWork.GetTotalQuota()
                , ct);
        }

        /// <summary>
        /// Dumps the customer identified by the specified customer ID to disk into a JSON (.jsonc) file.
        /// Does nothing in case of any error.
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="iCustomerId">Customer ID.</param>
        /// <returns></returns>
        public async Task DumpCustomer(CancellationToken ct, int iCustomerId)
        {
            await Task.Run(
                () =>
                {
                    var customer = _unitOfWork.GetCustomer(iCustomerId);
                    if (customer != null)
                        DumpHelper.DumpCustomer(customer);
                }
                , ct);
        }

        /// <summary>
        /// Dumps full names of all customers to disk into a text (.txt) file.
        /// The file will contain lines of full names, one full name per one line.
        /// Does nothing in case of any error.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task DumpAllFullNames(CancellationToken ct)
        {
            await Task.Run(
                () =>
                {
                    var customers = _unitOfWork.GetAllCustomers();
                    DumpHelper.DumpCustomersFullName(customers);
                }
                , ct);
        }

        /// <summary>
        /// Updates quotas of all customers and returns all customers with the quotas updated.
        /// </summary>
        /// <param name="ct"></param>
        /// <returns>All customers with the quotas updated or nothing in case of any error.</returns>
        public async Task<IEnumerable<Customer>> UpdateAllQuotas(CancellationToken ct)
        {
            return await Task.Run(
                () =>
                _unitOfWork.UpdateAllQuotas()
                , ct);
        }
    }
}
