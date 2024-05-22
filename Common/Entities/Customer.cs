using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string name, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn)
        {
            Name = name;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public Customer()
        {
            
        }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Account Balance")]
        public double AccountBalance { get; set; }

        [Display(Name = "Deluxe Account")]
        public bool DeluxeAccount { get; set; }

        [Display(Name = "Registered on")]
        public DateTime RegisteredOn { get; set; }

        public int TotalOrders;
    }
}
