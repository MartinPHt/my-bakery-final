using BakeryApi.Repositories;
using Common.CommConstants;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageBakersController : ControllerBase
    {
        public ManageBakersController()
        {
            
        }

        [HttpPost(Endpoints.CreateBakerEndPoint)]
        public IActionResult CreateBaker(CreateBakerRequest request)
        {
            try
            {
                //Save to database
                BakersRepository bakersRepo = new BakersRepository();
                Baker baker = new Baker(request.FirstName, request.LastName, request.EmailAddress, request.Salary, request.IsFullTime, request.RegisteredOn);
                bakersRepo.Save(baker);

                //generate response
                var response = GenerateResponse(baker);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.GetBakerEndPoint)]
        public IActionResult Get(GetBakerRequest request)
        {
            try
            {
                //Retrieve from database
                BakersRepository repo = new BakersRepository();
                Baker baker = repo.GetAll(n => n.Id == request.Id).Find(i => i.Id == request.Id);

                //generate response
                var response = GenerateResponse(baker);

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.GetAllBakersEndPoint)]
        public IActionResult GetAll()
        {
            try
            {
                BakersRepository bakersRepo = new BakersRepository();
                OrdersRepository ordersRepo = new OrdersRepository();
                List<Baker> allBakers = bakersRepo.GetAll(i => true);
                List<Order> allOrders = ordersRepo.GetAll(i => true);

                foreach (var baker in allBakers)
                {
                    int currentCount = 0;
                    foreach (var order in allOrders)
                    {
                        if (order.Baker_ID == baker.Id)
                        {
                            currentCount++;
                        }
                    }

                    baker.TotalOrders = currentCount;
                }

                var response = allBakers.Select(baker => GenerateResponse(baker)).ToList();

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.UpdateBakerEndPoint)]
        public IActionResult Update(UpdateBakerRequest request)
        {
            try
            {
                BakersRepository repo = new BakersRepository();
                var baker = new Baker()
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailAddress = request.EmailAddress,
                    Salary = request.Salary,
                    IsFullTime = request.IsFullTime,
                    RegisteredOn = request.RegisteredOn
                };
                repo.Save(baker);

                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.DeleteBakerEndPoint)]
        public IActionResult Delete(DeleteBakerRequest request)
        {
            try
            {
                BakersRepository repo = new BakersRepository();

                Baker baker = new Baker();
                baker.Id = request.Id;

                repo.Delete(baker);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.SearchBakersByFirstNameEndPoint)]
        public IActionResult Srearch(SearchBakersByFirstNameRequest request)
        {
            try
            {
                BakersRepository repo = new BakersRepository();
                List<Baker> bakersSearchResult = repo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(request.FirstName.ToUpper()));

                var response = bakersSearchResult.Select(baker => GenerateResponse(baker)).ToList();

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private BakerResponse GenerateResponse(Baker baker)
        {
            var response = new BakerResponse();
            response.Id = baker.Id;
            response.FirstName = baker.FirstName;
            response.LastName = baker.LastName;
            response.EmailAddress = baker.EmailAddress;
            response.Salary = baker.Salary;
            response.IsFullTime = baker.IsFullTime;
            response.RegisteredOn = baker.RegisteredOn;
            response.TotalOrders = baker.TotalOrders;

            return response;
        }
    }
}
