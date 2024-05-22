using MyBakeryFinal.Models;
using MyBakeryFinal.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyBakeryFinal.ExtensionMethods;
using Common.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;

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
            IndexVM vm = new IndexVM();
            //         List<Customer> allCustomers;

            //CustomersRepository customersRepo = new CustomersRepository();
            //         OrdersRepository ordersRepository = new OrdersRepository();
            //         RecipesRepository recipesRepository = new RecipesRepository();
            //RecipesToOrdersRepository recipesToOrdersRepository = new RecipesToOrdersRepository();
            //         List<Order> Orders = ordersRepository.GetAll(i => true);
            //List<Recipe> recipes = recipesRepository.GetAll(i => true);
            //allCustomers = customersRepo.GetAll(i => true);



            //         foreach (var order in Orders)
            //         {
            //             Customer customer = allCustomers.Find(c => c.Id == order.Customer_ID);
            //             order.Customer = customer;
            //         }

            //         vm.Orders = Orders;

            //List<RecipesToOrders> allOrdersRelations = recipesToOrdersRepository.GetAll(item => true);
            //var ordersToRecipes = new Dictionary<int, List<string>>();
            //         foreach (var entry in allOrdersRelations)
            //         {
            //             Recipe recipe = recipes.Find(r => r.Id == entry.Recipe_ID);
            //             if (ordersToRecipes.ContainsKey(entry.Order_ID))
            //             {

            //                 ordersToRecipes[entry.Order_ID].Add(recipe.Name);
            //             }
            //             else
            //             {
            //                 ordersToRecipes.Add(entry.Order_ID, new List<string>() { recipe.Name });
            //             }
            //         }

            //         vm.OrderToRecipes = ordersToRecipes;
            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddVM vm = new AddVM();
            //List<Customer> allCustomers = new List<Customer>();
            //List<Recipe> allRecipes = new List<Recipe>();

            //await Task.Run(() =>
            //{
            //    CustomersRepository customersRepo = new CustomersRepository();
            //    RecipesRepository recipesRepo = new RecipesRepository();
            //    allCustomers = customersRepo.GetAll(i => true);
            //    allRecipes = recipesRepo.GetAll(i => true);
            //});

            //vm.Customers = allCustomers;
            //vm.Recipes = allRecipes;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddVM addVM)
        {

            //OrdersRepository ordersRepo = new OrdersRepository();
            //CustomersRepository customersRepo = new CustomersRepository();
            //RecipesToOrdersRepository recipesToOrdersRepo = new RecipesToOrdersRepository();


            //Order item = new Order();
            //RecipesToOrders recipesToOrdersItem1 = new RecipesToOrders();
            //RecipesToOrders recipesToOrdersItem2 = new RecipesToOrders();
            //Customer customer = customersRepo.GetAll(customer => customer.Id == addVM.Customer_ID).FirstOrDefault();

            //if (customer == null)
            //{
            //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}

            //item.Tip = addVM.Tip;
            //item.isExpress = addVM.isExpress;
            //item.Details = addVM.Details;
            //item.Customer_ID = addVM.Customer_ID;

            //ordersRepo.Save(item);

            //if (addVM.Recipe1ID != 0)
            //{
            //    recipesToOrdersItem1.Order_ID = item.Id;
            //    recipesToOrdersItem1.Recipe_ID = addVM.Recipe1ID;
            //    recipesToOrdersRepo.Save(recipesToOrdersItem1);
            //}

            //if (addVM.Recipe2ID != 0)
            //{
            //    recipesToOrdersItem2.Order_ID = item.Id;
            //    recipesToOrdersItem2.Recipe_ID = addVM.Recipe2ID;
            //    recipesToOrdersRepo.Save(recipesToOrdersItem2);
            //}


            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            //OrdersRepository ordersRepo = new OrdersRepository();
            //RecipesRepository recipesRepository = new RecipesRepository();
            //CustomersRepository customersRepo = new CustomersRepository();

            //Order order = ordersRepo.GetAll(order => order.Id == id).FirstOrDefault();
            //List<Customer> customers = customersRepo.GetAll(b => true).ToList();
            //List<Recipe> allRecipes = recipesRepository.GetAll(r => true).ToList();

            EditVM vm = new EditVM();
            //vm.Order = order;
            //vm.Customers = customers;
            //vm.Recipes = allRecipes;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditVM vm)
        {
            //OrdersRepository ordersRepo = new OrdersRepository();
            //RecipesRepository recipesRepo = new RecipesRepository();
            //CustomersRepository customersRepo = new CustomersRepository();
            //RecipesToOrdersRepository recipesToOrdersRepository = new RecipesToOrdersRepository();

            //List<RecipesToOrders> allRecipesToOrders = recipesToOrdersRepository.GetAll(r => true).ToList();
            //Customer customer = customersRepo.GetAll(customer => customer.Id == vm.Customer_ID).FirstOrDefault();
            //RecipesToOrders recipesToOrders = new RecipesToOrders();

            //vm.Order.Customer = customer;

            //if (vm.Recipe1ID != 0)
            //{

            //    RecipesToOrders recipeToOrder = allRecipesToOrders.Find(entry => entry.Order_ID == vm.Order.Id && entry.Recipe_ID == vm.Recipe1ID);
            //    if (recipeToOrder == null)
            //    {
            //        recipeToOrder = new RecipesToOrders();
            //        recipeToOrder.Order_ID = vm.Order.Id;
            //    }
            //    recipeToOrder.Recipe_ID = vm.Recipe1ID;
            //    recipesToOrdersRepository.Save(recipeToOrder);

            //}

            //if (vm.Recipe2ID != 0)
            //{

            //    RecipesToOrders recipeToOrder = allRecipesToOrders.Find(entry => entry.Order_ID == vm.Order.Id && entry.Recipe_ID == vm.Recipe2ID);

            //    if (recipeToOrder == null)
            //    {
            //        recipeToOrder = new RecipesToOrders();
            //        recipeToOrder.Order_ID = vm.Order.Id;
            //    }

            //    recipeToOrder.Recipe_ID = vm.Recipe2ID;
            //    recipesToOrdersRepository.Save(recipeToOrder);

            //}

            //ordersRepo.Save(vm.Order);

            return RedirectToAction("Index");

        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            //OrdersRepository repo = new OrdersRepository();
            //Order order = new Order();
            //order.Id = id;

            //repo.Delete(order);

            return RedirectToAction("Index");
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
    }
}
