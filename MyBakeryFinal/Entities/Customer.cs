using MyBakeryFinal.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyBakeryFinal.Entities
{
    public class Customer : BaseEntity
    {

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        public int TotalOrders;


    }
}
