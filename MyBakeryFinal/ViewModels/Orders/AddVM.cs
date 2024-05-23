using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Common.Entities;

namespace MyBakeryFinal.ViewModels.Orders
{
    public class AddVM
    {

        public AddVM()
        {
            Customers = new List<Customer>();
            Bakers = new List<Baker>();
        }

        [DisplayName("Details: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Details { get; set; }

        [DisplayName("Quantity: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Quantity { get; set; }

        [DisplayName("Tip (BGN): ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Tip { get; set; }

        [DisplayName("Total (BGN): ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Total { get; set; }

        [DisplayName("Express Delivery: ")]
        public bool IsExpress { get; set; }

        [DisplayName("Customer: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Customer_ID { get; set; }

        public List<Customer> Customers { get; set; }

        [DisplayName("Baker: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Baker_ID { get; set; }

        public List<Baker> Bakers { get; set; }
    }
}
