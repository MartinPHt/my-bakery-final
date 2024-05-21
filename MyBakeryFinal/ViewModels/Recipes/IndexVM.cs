using MyBakeryFinal.Entities;
namespace MyBakeryFinal.ViewModels.Recipes
{
    public class IndexVM
    {
        public List<Recipe> Recipes { get; set; }
        public Dictionary<int, int> RecipesToCount { get; set; }
    }
}
