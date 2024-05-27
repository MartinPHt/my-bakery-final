using Common.Entities;

namespace MyBakeryFinal.ViewModels.Orders
{
    public class SearchVM
    {
        public string Filter = "Details";
        public List<string> AllProperties { get; } = new List<string>() { "Details", "Customer", "Baker" };
        public List<Order> Orders { get; set; }
    }
}
