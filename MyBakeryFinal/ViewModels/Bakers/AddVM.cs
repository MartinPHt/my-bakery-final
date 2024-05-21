using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyBakeryFinal.ViewModels.Bakers
{
    public class AddVM
    {

        [DisplayName("First name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }

        [DisplayName("Last name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }
    }
}
