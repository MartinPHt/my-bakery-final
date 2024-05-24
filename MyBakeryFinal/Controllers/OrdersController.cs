using MyBakeryFinal.Models;
using MyBakeryFinal.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using Common.CommConstants;
using MyBakeryFinal.Services;
using System.Text;

namespace MyBakeryFinal.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await OrderService.Instance.GetAllAsync<List<OrderResponse>>();
                if (response == null)
                    return BadRequest("Couldn't load orders. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allOrders = response.Select(orderResponse => GenerateOrder(orderResponse)).ToList();

                vm.Orders = allOrders;
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
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Retreive all customers
            var customersResponse = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

            if (customersResponse == null)
                return BadRequest("Couldn't load customers. Responce message from the server is null");

            //Retreive all Bakers
            var bakersResponse = await BakerService.Instance.GetAllAsync<List<BakerResponse>>();

            if (bakersResponse == null)
                return BadRequest("Couldn't load bakers. Responce message from the server is null");

            AddVM vm = new AddVM();
            vm.Customers = customersResponse.Select(customerResponse => GenerateCustomer(customerResponse)).ToList();
            vm.Bakers = bakersResponse.Select(bakerResponse => GenerateBaker(bakerResponse)).ToList();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddVM addVM)
        {
            try
            {
                var response = await OrderService.Instance.PostAsync<OkResult>(new CreateOrderRequest(addVM.Details, addVM.Quantity, addVM.Tip, addVM.Total, addVM.IsExpress, DateTime.Now, addVM.Customer_ID, addVM.Baker_ID));

                if (response == null)
                    return BadRequest("Couldn't add order. Responce message from the server is null");

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
            var orderResponse = await OrderService.Instance.GetAsync<OrderResponse>(id.ToString());

            EditVM vm = new EditVM();
            vm.Order = GenerateOrder(orderResponse);


            //Retreive all customers
            var customersResponse = await CustomerService.Instance.GetAllAsync<List<CustomerResponse>>();

            if (customersResponse == null)
                return BadRequest("Couldn't load customers which are needed for editing orders. Responce message from the server is null");

            //Retreive all Bakers
            var bakersResponse = await BakerService.Instance.GetAllAsync<List<BakerResponse>>();

            if (bakersResponse == null)
                return BadRequest("Couldn't load bakers which are needed for editing orders. Responce message from the server is null");

            vm.Customers = customersResponse.Select(customerResponse => GenerateCustomer(customerResponse)).ToList();
            vm.Bakers = bakersResponse.Select(bakerResponse => GenerateBaker(bakerResponse)).ToList();

            //Generate html raw for select containers for Baker and Customer
            //with preselected value matching the one in the database
            var optionsHtml = new StringBuilder();
            foreach (var baker in vm.Bakers)
            {
                var selected = baker.Id == vm.Order.Baker_ID ? "selected=\"selected\"" : "";
                optionsHtml.Append($"<option {selected} value=\"{baker.Id}\">{baker.FirstName} {baker.LastName}</option>");
            }
            ViewBag.BakerOptions = optionsHtml.ToString();

            optionsHtml = new StringBuilder();
            foreach (var customer in vm.Customers)
            {
                var selected = customer.Id == vm.Order.Customer_ID ? "selected=\"selected\"" : "";
                optionsHtml.Append($"<option {selected} value=\"{customer.Id}\">{customer.FirstName} {customer.LastName}</option>");
            }
            ViewBag.CustomerOptions = optionsHtml.ToString();

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await OrderService.Instance.PutAsync<OkResult>(vm.Order.Id, new UpdateOrderRequest(vm.Order.Id, vm.Order.Details, vm.Order.Quantity, vm.Order.Tip, vm.Order.Total, vm.Order.IsExpress, vm.Order.PlacedOn, vm.Order.Customer_ID, vm.Order.Baker_ID));

                if (response == null)
                    return BadRequest("Couldn't edit Order. Responce message from the server is null");

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
                var response = await OrderService.Instance.DeleteAsync<OkResult>(id.ToString());

                if (response == null)
                    return BadRequest("Couldn't delete order. Responce message from the server is null");

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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Search(string firstName)
        {
            try
            {
                var responseList = await OrderService.Instance.GetSearchAsync<List<OrderResponse>>(firstName);

                if (responseList == null)
                    return BadRequest("Couldn't search for orders. Responce message from the server is null");

                SearchVM vm = new SearchVM();
                var ordersList = responseList.Select(response => GenerateOrder(response)).ToList();

                vm.Orders = ordersList;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Order GenerateOrder(OrderResponse responce)
        {
            return new Order()
            {
                Id = responce.Id,
                Details = responce.Details,
                Tip = responce.Tip,
                Total = responce.Total,
                IsExpress = responce.IsExpress,
                PlacedOn = responce.PlacedOn,
                Quantity = responce.Quantity,

                //Foreign Keys
                Customer = responce.Customer,
                Customer_ID = responce.Customer.Id,
                Baker = responce.Baker,
                Baker_ID = responce.Baker.Id,
            };
        }

        private Customer GenerateCustomer(CustomerResponse customerResponse)
        {
            return new Customer()
            {
                Id = customerResponse.Id,
                FirstName = customerResponse.FirstName,
                LastName = customerResponse.LastName,
                Address = customerResponse.Address,
                AccountBalance = customerResponse.AccountBalance,
                DeluxeAccount = customerResponse.DeluxeAccount,
                RegisteredOn = customerResponse.RegisteredOn,
            };
        }

        private Baker GenerateBaker(BakerResponse bakerResponse)
        {
            return new Baker()
            {
                Id = bakerResponse.Id,
                FirstName = bakerResponse.FirstName,
                LastName = bakerResponse.LastName,
                EmailAddress = bakerResponse.EmailAddress,
                Salary = bakerResponse.Salary,
                IsFullTime = bakerResponse.IsFullTime,
                RegisteredOn = bakerResponse.RegisteredOn,
            };
        }


    }
}
