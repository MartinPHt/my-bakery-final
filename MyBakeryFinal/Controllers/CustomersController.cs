using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBakeryFinal.Entities;
using MyBakeryFinal.Models;
using MyBakeryFinal.Repositories;
using MyBakeryFinal.ViewModels.Customers;
using System.Diagnostics;

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
        public IActionResult Index()
        {
            IndexVM vm = new IndexVM();

            var allCustomers = new List<Customer>(); //TODO: get from back end

            vm.Customers = allCustomers;
            return View(vm);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddVM addVM)
        {
            Customer item = new Customer();
            item.FirstName = addVM.FirstName;
            item.LastName = addVM.LastName;
            item.Address = addVM.Address;

            //TODO: Update using api customersRepo.Save(item);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            //TODO: Get customer from api;
            Customer customer = new Customer();
            EditVM vm = new EditVM();
            vm.Customer = customer;

            return View(vm);

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditVM vm)
        {
            //TODO: Save the customer using the api repo.Save(vm.Customer);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            Customer customer = new Customer();
            customer.Id = id;

            // Use the api to delete: repo.Delete(customer);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}