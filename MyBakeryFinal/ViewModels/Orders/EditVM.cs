using MyBakeryFinal.Entities;
using System.ComponentModel;

namespace MyBakeryFinal.ViewModels.Orders
{
	public class EditVM
	{
		public Order Order { get; set; }

		public int Customer_ID { get; set; }
		public List<Customer> Customers { get; set; }

		public List<Recipe> Recipes { get; set; }

		[DisplayName("Recipe 1")]
		public int Recipe1ID { get; set; }

		[DisplayName("Recipe 2")]
		public int Recipe2ID { get; set; }
	}
}
