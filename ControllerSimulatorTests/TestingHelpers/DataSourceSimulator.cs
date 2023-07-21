using ControllerSimulator.DataAccess;

namespace ControllerSimulator
{
    /// <summary>
    /// Some hypotetic data-source generator.
    /// </summary>
    public sealed class DataSourceSimulator
    {
        /// <summary>
        /// Produces raw data of all our customers.
        /// </summary>
        /// <returns>The array of all customers that we have.</returns>
        public static RoughCustomer[] GetAllCustomers()
        {
            return new RoughCustomer[]
            {
            new RoughCustomer
            {
                id = 1,
                firstName = "John",
                middleName = "",
                lastName = "Doe",
                born = new DateTime(1960,1,24),
                rank = 2,
                quota = 10
            },
            new RoughCustomer
            {
                id = 2,
                firstName = "Jane",
                middleName = "",
                lastName = "Smith",
                born = new DateTime(1970,3,12),
                rank = 1,
                quota = 3
            },
            new RoughCustomer
            {
                id = 3,
                firstName = "Joe",
                middleName = "Walter",
                lastName = "Black",
                born = new DateTime(1980,5,21),
                rank = 0,
                quota = 5
            },
            new RoughCustomer
            {
                id = 4,
                firstName = "Иван",
                middleName = "Иванович",
                lastName = "Иванов",
                born = new DateTime(1976,10,23),
                rank = 1,
                quota = 9
            }
            };
        }
    }
}
