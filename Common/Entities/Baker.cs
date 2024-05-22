using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Baker: BaseEntity
    {

        [Display(Name="First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public int TotalRecipes;

    }
}
