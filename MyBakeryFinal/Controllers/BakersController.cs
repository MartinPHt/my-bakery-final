using MyBakeryFinal.Models;
using MyBakeryFinal.ViewModels.Bakers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyBakeryFinal.Entities;
using MyBakeryFinal.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace MyBakeryFinal.Controllers
{
    public class BakersController : Controller
    {
        private readonly ILogger<BakersController> _logger;

        public BakersController(ILogger<BakersController> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index()
        {
            IndexVM vm = new IndexVM();
            BakersRepository bakersRepo = new BakersRepository();
            RecipesRepository recipesRepo = new RecipesRepository();
            List<Baker> allBakers = bakersRepo.GetAll(i => true);
            List<Recipe> allRecipes = recipesRepo.GetAll(i => true);

            foreach (var baker in allBakers)
            {
                int currentCount = 0;
                foreach (var recipe in allRecipes)
                {
                    if (recipe.Baker_ID == baker.Id)
                    {
                        currentCount++;
                    }
                }

                baker.TotalRecipes = currentCount;
            }

            vm.Bakers = allBakers;



            return View(vm);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddVM addVM)
        {

            BakersRepository bakersRepo = new BakersRepository();

            Baker item = new Baker();
            item.FirstName = addVM.FirstName;
            item.LastName = addVM.LastName;

            bakersRepo.Save(item);


            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            BakersRepository repo = new BakersRepository();
            Baker baker = repo.GetAll(baker => baker.Id == id).Find(i => i.Id == id);

            EditVM vm = new EditVM();
            vm.Baker = baker;

            return View(vm);

        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(EditVM vm)
        {
            BakersRepository repo = new BakersRepository();

            repo.Save(vm.Baker);

            return RedirectToAction("Index");

        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            BakersRepository repo = new BakersRepository();

            Baker baker = new Baker();
            baker.Id = id;

            repo.Delete(baker);

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
