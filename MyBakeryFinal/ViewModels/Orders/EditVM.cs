using Common.Entities;
using System.ComponentModel;

namespace MyBakeryFinal.ViewModels.Orders
{
	public class EditVM
	{
        public Order Order { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public List<Baker> Bakers { get; set; } = new List<Baker>();
    }
}
