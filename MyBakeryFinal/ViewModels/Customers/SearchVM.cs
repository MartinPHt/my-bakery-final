using Common.Entities;

namespace MyBakeryFinal.ViewModels.Customers
{
    public class SearchVM
    {
        public string Filter = "Address";
        public List<string> AllProperties { get; } = new List<string>() { "Address", "First Name", "Last Name" };
        public List<Customer> Customers { get; set; }
    }
}
