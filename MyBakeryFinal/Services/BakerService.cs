namespace MyBakeryFinal.Services
{
    public class BakerService : BaseService
    {
        public static BakerService Instance { get; } = new BakerService();

        public BakerService() : base("ManageBakers")
        {

        }
    }
}
