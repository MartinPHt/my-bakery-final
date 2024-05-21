using Common.CommConstants;
using Microsoft.AspNetCore.Mvc;
using BakeryApi.Entities;
using BakeryApi.Repositories;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersManagementController : ControllerBase
    {
        public CustomersManagementController()
        {

        }

        // Create/Edit
        [HttpPost]
        [EndpointName(Endpoints.CreateCustomerEndPoint)]
        public JsonResult CreateEdit(Customer customer)
        {
            //TODO
            return new JsonResult(Ok());
        }

        [HttpGet]
        [EndpointName(Endpoints.GetCustomerEndPoint)]
        public JsonResult Get(int id)
        {
            //TODO
            return new JsonResult(Ok());
        }

        [HttpGet]
        [EndpointName(Endpoints.GetAllCustomerEndPoint)]
        public JsonResult GetAll()
        {
            CustomersRepository customersRepo = new CustomersRepository();
            OrdersRepository ordersRepo = new OrdersRepository();
            List<Customer> allCustomers = customersRepo.GetAll(i => true);
            List<Order> allOrders = ordersRepo.GetAll(i => true);

            foreach (var customer in allCustomers)
            {
                int currentCount = 0;
                foreach (var order in allOrders)
                {
                    if (order.Customer_ID == customer.Id)
                    {
                        currentCount++;
                    }
                }

                customer.TotalOrders = currentCount;
            }
            return new JsonResult(Ok(allCustomers));
        }

        [HttpPut]
        [EndpointName(Endpoints.UpdateCustomerEndPoint)]
        public JsonResult Update(Customer customer)
        {
            //TODO
            return new JsonResult(Ok());
        }

        [HttpDelete]
        [EndpointName(Endpoints.DeleteCustomerEndPoint)]
        public JsonResult Delete(Customer customer)
        {
            //TODO
            return new JsonResult(Ok());
        }
    }
}
