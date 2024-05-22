namespace MyBakeryFinal.Services
{
    public class ManageOrdersService : BaseService
    {
        public static ManageOrdersService Instance { get; } = new ManageOrdersService();

        public ManageOrdersService() : base("ManageOrders")
        {

        }
    }
}
