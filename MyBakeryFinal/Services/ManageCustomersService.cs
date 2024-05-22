namespace MyBakeryFinal.Services
{
    public class ManageCustomersService : BaseService
    {

        public static ManageCustomersService Instance { get; } = new ManageCustomersService();

        public ManageCustomersService() : base("ManageCustomers")
        {

        }
    }
}
