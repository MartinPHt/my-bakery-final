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
                Customer customer = new Customer(request.FirstName, request.LastName, request.Address, request.AccountBalance, request.DeluxeAccount, request.RegisteredOn);
                customersRepo.Save(customer);

                //generate response
                var response = GenerateResponse(customer);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.GetCustomerEndPoint)]
        public IActionResult Get(GetCustomerRequest request)
        {
            try
            {
                //Retrieve from database
                CustomersRepository repo = new CustomersRepository();
                Customer customer = repo.GetAll(n => n.Id == request.Id).Find(i => i.Id == request.Id);

                //generate response
                var response = GenerateResponse(customer);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.GetAllCustomersEndPoint)]
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

                var response = allCustomers.Select(customer => GenerateResponse(customer)).ToList();

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.UpdateCustomerEndPoint)]
        public IActionResult Update(UpdateCustomerRequest request)
        {
            try
            {
                CustomersRepository repo = new CustomersRepository();
                var customer = new Customer()
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    AccountBalance = request.AccountBalance,
                    DeluxeAccount = request.DeluxeAccount,
                    RegisteredOn = request.RegisteredOn
                };
                repo.Save(customer);

                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.DeleteCustomerEndPoint)]
        public IActionResult Delete(DeleteCustomerRequest request)
        {
            try
            {
                CustomersRepository repo = new CustomersRepository();

                Customer customer = new Customer();
                customer.Id = request.Id;

                repo.Delete(customer);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.SearchCustomersByFirstNameEndPoint)]
        public IActionResult Srearch(SearchCustomersByFirstNameRequest request)
        {
            try
            {
                CustomersRepository repo = new CustomersRepository();
                List<Customer> customersSearchResult = repo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(request.FirstName.ToUpper()));

                var response = customersSearchResult.Select(customer => GenerateResponse(customer)).ToList();

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private CustomerResponse GenerateResponse(Customer customer)
        {
            var response = new CustomerResponse();
            response.Id = customer.Id;
            response.FirstName = customer.FirstName;
            response.LastName = customer.LastName;
            response.Address = customer.Address;
            response.AccountBalance = customer.AccountBalance;
            response.DeluxeAccount = customer.DeluxeAccount;
            response.RegisteredOn = customer.RegisteredOn;
            response.TotalOrders = customer.TotalOrders;

            return response;
        }
    }
}
