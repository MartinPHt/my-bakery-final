using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MyBakeryFinal.Entities;

namespace MyBakeryFinal.ViewModels.Recipes
{
    public class AddVM
    {


        [DisplayName("Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Name { get; set; }

        [DisplayName("Price: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public double Price { get; set; }

        [DisplayName("Description: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Description { get; set; }


        [DisplayName("Baker ID: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int Baker_ID { get; set; }

        public List<Baker> Bakers { get; set; }
    }
}
