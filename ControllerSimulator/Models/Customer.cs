using ControllerSimulator.DataAccess;
using Newtonsoft.Json;

namespace ControllerSimulator.Models
{
    /// <summary>
    /// Customer class, the class that must be designed for handling and
    /// transmission over Web API with JSON serialization/de-serialization.
    /// </summary>
    public abstract class Customer
    {
        public Customer(RoughCustomer roughCustomer)
        {
            Id = roughCustomer.id;
            FirstName = roughCustomer.firstName;
            MiddleName = roughCustomer.middleName;
            LastName = roughCustomer.lastName;
            DateOfBirth = roughCustomer.born;
            Rank = (Rank)roughCustomer.rank;
            Quota = roughCustomer.quota;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [JsonProperty("born")]
        public DateTime DateOfBirth { get; set; }

        public Rank Rank { get; set; }

        public int Quota { get; set; }

        [JsonIgnore]
        public string FullName => FirstName + " " + MiddleName + " " + LastName;

        [JsonIgnore]
        public static CustomerFactory Factory { get; } = new CustomerFactory();

        public abstract void UpdateQuota();
    }
}
