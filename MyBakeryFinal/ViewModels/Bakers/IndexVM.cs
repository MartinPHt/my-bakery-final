using Common.Entities;

namespace MyBakeryFinal.ViewModels.Bakers
{
    public class IndexVM
    {
        public string Filter = "E-mail";
        public List<string> AllProperties { get; } = new List<string>() { "E-mail", "First Name", "Last Name" };
        public List<Baker> Bakers { get; set; }
    }
}
