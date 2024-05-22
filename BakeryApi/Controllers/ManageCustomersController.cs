﻿using Common.CommConstants;
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

                var response = allCustomers.Select(customer => new CustomerResponse()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    AccountBalance = customer.AccountBalance,
                    DeluxeAccount = customer.DeluxeAccount,
                    RegisteredOn =customer.RegisteredOn,
                }).ToList();

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
                    Name = request.Name,
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

        private CustomerResponse GenerateResponse(Customer customer)
        {
            var response = new CustomerResponse();
            response.Id = customer.Id;
            response.Name = customer.Name;
            response.Address = customer.Address;
            response.AccountBalance = customer.AccountBalance;
            response.DeluxeAccount = customer.DeluxeAccount;
            response.RegisteredOn = customer.RegisteredOn;

            return response;
        }
    }
}
