using Common.CommConstants;
using Microsoft.AspNetCore.Mvc;
using Common.Entities;
using BakeryApi.Repositories;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCustomersController : ControllerBase
    {
        private readonly CustomersRepository _customersRepo;
        private readonly OrdersRepository _ordersRepo;

        public ManageCustomersController()
        {
            _customersRepo = new CustomersRepository();
            _ordersRepo = new OrdersRepository();
        }

        // Create
        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                var customer = new Customer(request.FirstName, request.LastName, request.Address, request.AccountBalance, request.DeluxeAccount, request.RegisteredOn);
                _customersRepo.Save(customer);

                var response = GenerateResponse(customer);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, response); // Return 201 Created
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Retrieve by ID
        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            try
            {
                var customer = _customersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (customer == null)
                {
                    return NotFound(); // Return 404 if not found
                }

                var response = GenerateResponse(customer);
                return Ok(response); // Return 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Retrieve all
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var allCustomers = _customersRepo.GetAll(i => true);
                var allOrders = _ordersRepo.GetAll(i => true);

                foreach (var customer in allCustomers)
                {
                    customer.TotalOrders = allOrders.Count(order => order.Customer_ID == customer.Id);
                }

                var response = allCustomers.Select(customer => GenerateResponse(customer)).ToList();
                return Ok(response); // Return 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            try
            {
                var customer = _customersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (customer == null)
                {
                    return NotFound(); // Return 404 if not found
                }

                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Address = request.Address;
                customer.AccountBalance = request.AccountBalance;
                customer.DeluxeAccount = request.DeluxeAccount;
                customer.RegisteredOn = request.RegisteredOn;

                _customersRepo.Save(customer);
                return Ok(); // Return 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = _customersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (customer == null)
                {
                    return NotFound();
                }

                _customersRepo.Delete(customer);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("search/{searchWord}")]
        public IActionResult SearchCustomersByFirstName(string searchWord)
        {
            try
            {
                var customersSearchResult = _customersRepo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));
                var response = customersSearchResult.Select(customer => GenerateResponse(customer)).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private CustomerResponse GenerateResponse(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                AccountBalance = customer.AccountBalance,
                DeluxeAccount = customer.DeluxeAccount,
                RegisteredOn = customer.RegisteredOn,
                TotalOrders = customer.TotalOrders
            };
        }
    }
}