using Common.CommConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.Entities;
using MyBakeryFinal.Models;
using MyBakeryFinal.Services;
using MyBakeryFinal.ViewModels.Customers;
using System.Diagnostics;
using System.Net.Http;

namespace MyBakeryFinal.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await ManageCustomersService.Instance.SendRequest<List<CustomerResponse>>(new GetAllCustomerRequest());

                if (response == null)
                    return BadRequest("Couldn't load customers. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allCustomers = new List<Customer>();

                foreach (var customerResponse in response)
                {
                    var customer = new Customer();
                    customer.Id = customerResponse.Id;
                    customer.Name = customerResponse.Name;
                    customer.Address = customerResponse.Address;
                    customer.AccountBalance = customerResponse.AccountBalance;
                    customer.DeluxeAccount = customerResponse.DeluxeAccount;
                    customer.RegisteredOn = customerResponse.RegisteredOn;
                    allCustomers.Add(customer);
                }

                vm.Customers = allCustomers;
                return View(vm);
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await ManageCustomersService.Instance.SendRequest<CustomerResponse>(new CreateCustomerRequest(addVM.Name, addVM.Address, addVM.AccountBalance, addVM.DeluxeAccount, DateTime.Now));

                if (response == null)
                    return BadRequest("Couldn't add customer. Responce message from the server is null");    

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await ManageCustomersService.Instance.SendRequest<CustomerResponse>(new GetCustomerRequest(id));

            Customer customer = new Customer()
            {
                Id = response.Id,
                Name = response.Name,
                Address = response.Address,
                AccountBalance = response.AccountBalance,
                DeluxeAccount = response.DeluxeAccount,
                RegisteredOn = response.RegisteredOn
            };
            EditVM vm = new EditVM();
            vm.Customer = customer;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await ManageCustomersService.Instance.SendRequest<OkResult>(new UpdateCustomerRequest(vm.Customer.Id, vm.Customer.Name, vm.Customer.Address, vm.Customer.AccountBalance, vm.Customer.DeluxeAccount, vm.Customer.RegisteredOn));

                if (response == null)
                    return BadRequest("Couldn't edit customer. Responce message from the server is null");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await ManageCustomersService.Instance.SendRequest<OkResult>(new DeleteCustomerRequest(id));

                if (response == null)
                    return BadRequest("Couldn't edit customer. Responce message from the server is null");

                return RedirectToAction("Index");
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { error = "External service is unavailable. Please try again later.", details = httpRequestException.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later.", details = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}