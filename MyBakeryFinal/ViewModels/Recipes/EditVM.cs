using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MyBakeryFinal.Entities;

namespace MyBakeryFinal.ViewModels.Recipes
{
    public class EditVM
    {
        public Recipe Recipe { get; set; }

        public int Baker_ID { get; set; }
        public List<Baker> Bakers { get; set; }
    }
}
