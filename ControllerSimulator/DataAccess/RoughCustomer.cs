namespace ControllerSimulator.DataAccess
{
    /// <summary>
    /// Raw structure of the customer data returned by some hypotetic
    /// data-source generator.
    /// </summary>
    public struct RoughCustomer
    {
        public int id; //unique id
        public string firstName;
        public string middleName;
        public string lastName;
        public DateTime born; //date of birth
        public int rank; //0 - regular, 1 - valuable, 2 - vip
        public int quota; //some hypotetic quota, cannot be less than zero
    }
}
