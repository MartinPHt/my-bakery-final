using Common.CommConstants;
using Microsoft.AspNetCore.Mvc;
using Common.Entities;
using BakeryApi.Repositories;
using System.Net;
using System.Collections.Generic;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCustomersController : ControllerBase
    {
        public ManageCustomersController()
        {
            
        }

        // Create/Edit
        [HttpPost(Endpoints.CreateCustomerEndPoint)]
        public IActionResult CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                //Save to database
                CustomersRepository customersRepo = new CustomersRepository();
                Customer customer = new Customer(request.Name, request.Address, request.AccountBalance, request.DeluxeAccount, request.RegisteredOn);
                customersRepo.Save(customer);

                //generate response
                var response = new CustomerResponse();
                response.Name = customer.Name;
                response.Address = customer.Address;
                response.AccountBalance = customer.AccountBalance;
                response.DeluxeAccount = customer.DeluxeAccount;
                response.RegisteredOn = customer.RegisteredOn;

                return new JsonResult(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request." });
            }
        }

        [HttpPost(Endpoints.GetCustomerEndPoint)]
        public JsonResult Get(int id)
        {
            //TODO
            return new JsonResult(Ok());
        }

        [HttpPost(Endpoints.GetAllCustomerEndPoint)]
        public IActionResult GetAll()
        {
            try
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

                List<CustomerResponse> response = new List<CustomerResponse>();
                foreach (var item in allCustomers)
                {
                    var customerResponse = new CustomerResponse();
                    customerResponse.Name = item.Name;
                    customerResponse.Address = item.Address;
                    customerResponse.AccountBalance = item.AccountBalance;
                    customerResponse.DeluxeAccount = item.DeluxeAccount;
                    customerResponse.RegisteredOn = item.RegisteredOn;
                    response.Add(customerResponse);
                }

                return new JsonResult(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request." });
            }
        }

        [HttpPost(Endpoints.UpdateCustomerEndPoint)]
        public JsonResult Update(Customer customer)
        {
            //TODO
            return new JsonResult(Ok());
        }

        [HttpPost(Endpoints.DeleteCustomerEndPoint)]
        public JsonResult Delete(Customer customer)
        {
            //TODO
            return new JsonResult(Ok());
        }
    }
}
