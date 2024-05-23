using MyBakeryFinal.Models;
using MyBakeryFinal.ViewModels.Bakers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Common.CommConstants;
using Common.Entities;
using MyBakeryFinal.Services;


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
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await BakerService.Instance.SendRequest<List<BakerResponse>>(new GetAllBakerRequest());

                if (response == null)
                    return BadRequest("Couldn't load bakers. Responce message from the server is null");

                IndexVM vm = new IndexVM();
                var allBakers = response.Select(bakerResponse => new Baker()
                {
                    Id = bakerResponse.Id,
                    FirstName = bakerResponse.FirstName,
                    LastName = bakerResponse.LastName,
                    EmailAddress = bakerResponse.EmailAddress,
                    Salary = bakerResponse.Salary,
                    IsFullTime = bakerResponse.IsFullTime,
                    RegisteredOn = bakerResponse.RegisteredOn,
                    TotalOrders = bakerResponse.TotalOrders,
                }).ToList();

                vm.Bakers = allBakers;
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
                var response = await BakerService.Instance.SendRequest<BakerResponse>(new CreateBakerRequest(addVM.FirstName, addVM.LastName, addVM.EmailAddress, addVM.Salary, addVM.IsFullTime, DateTime.Now));

                if (response == null)
                    return BadRequest("Couldn't add baker. Responce message from the server is null");    

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
            var response = await BakerService.Instance.SendRequest<BakerResponse>(new GetBakerRequest(id));

            Baker baker = new Baker()
            {
                Id = response.Id,
                FirstName = response.FirstName,
                LastName = response.LastName,
                EmailAddress = response.EmailAddress,
                Salary = response.Salary,
                IsFullTime = response.IsFullTime,
                RegisteredOn = response.RegisteredOn
            };
            EditVM vm = new EditVM();
            vm.Baker = baker;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditVM vm)
        {
            try
            {
                var response = await BakerService.Instance.SendRequest<OkResult>(new UpdateBakerRequest(vm.Baker.Id, vm.Baker.FirstName, vm.Baker.LastName, vm.Baker.EmailAddress, vm.Baker.Salary, vm.Baker.IsFullTime, vm.Baker.RegisteredOn));

                if (response == null)
                    return BadRequest("Couldn't edit baker. Responce message from the server is null");

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
                var response = await BakerService.Instance.SendRequest<OkResult>(new DeleteBakerRequest(id));

                if (response == null)
                    return BadRequest("Couldn't edit baker. Responce message from the server is null");

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
                var responseList = await BakerService.Instance.SendRequest<List<BakerResponse>>(new SearchBakersByFirstNameRequest(firstName));

                if (responseList == null)
                    return BadRequest("Couldn't add Baker. Responce message from the server is null");

                SearchVM vm = new SearchVM();
                var BakersList = responseList.Select(response => new Baker()
                {
                    Id = response.Id,
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    EmailAddress = response.EmailAddress,
                    Salary = response.Salary,
                    IsFullTime = response.IsFullTime,
                    RegisteredOn = response.RegisteredOn,
                }).ToList();

                vm.Bakers = BakersList;
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
    }
}
