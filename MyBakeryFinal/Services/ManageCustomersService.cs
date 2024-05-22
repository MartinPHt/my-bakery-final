using Common.CommConstants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
