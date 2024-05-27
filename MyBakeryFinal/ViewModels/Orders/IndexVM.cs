using Common.Entities;

namespace MyBakeryFinal.ViewModels.Orders
{
    public class IndexVM
    {
        public string Filter = "Details";
        public List<string> AllProperties { get; } = new List<string>() { "Details", "Customer", "Baker" }; 
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
