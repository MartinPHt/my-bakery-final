using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MyBakeryFinal.Entities;

namespace MyBakeryFinal.ViewModels.Orders
{
    public class AddVM
    {

        public AddVM()
        {
            Customers = new List<Customer>();
        }

        [DisplayName("Tip: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Tip { get; set; }

        [DisplayName("Details: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Details { get; set; }


        [DisplayName("Customer: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Customer_ID { get; set; }

        [DisplayName("Express Delivery: ")]
 
        public bool isExpress { get; set; }

        public List<Customer> Customers { get; set; }

        public List<Recipe> Recipes { get; set; }

        [DisplayName("Recipe 1")]
        public int Recipe1ID { get; set; }

		[DisplayName("Recipe 2")]
		public int Recipe2ID { get; set; }
    }
}
