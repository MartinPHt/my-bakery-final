namespace MyBakeryFinal.Services
{
    public class ManageBakersService : BaseService
    {
        public static ManageBakersService Instance { get; } = new ManageBakersService();

        public ManageBakersService() : base("ManageBakers")
        {

        }
    }
}
