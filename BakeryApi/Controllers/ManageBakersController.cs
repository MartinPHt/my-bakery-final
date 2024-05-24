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

        [HttpPost]
        public IActionResult CreateBaker(CreateBakerRequest request)
        {
            try
            {
                // Save to database
                BakersRepository bakersRepo = new BakersRepository();
                Baker baker = new Baker(request.FirstName, request.LastName, request.EmailAddress, request.Salary, request.IsFullTime, request.RegisteredOn);
                bakersRepo.Save(baker);

                // Generate response
                var response = GenerateResponse(baker);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBaker(int id)
        {
            try
            {
                // Retrieve from database
                BakersRepository repo = new BakersRepository();
                Baker baker = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (baker == null)
                {
                    return NotFound();
                }

                // Generate response
                var response = GenerateResponse(baker);
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAllBakers()
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

        [HttpPut("{id}")]
        public IActionResult UpdateBaker(int id, UpdateBakerRequest request)
        {
            try
            {
                BakersRepository repo = new BakersRepository();

                // Find the existing baker object
                var existingBaker = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (existingBaker == null)
                {
                    return NotFound();
                }

                // Update the existing baker object
                existingBaker.FirstName = request.FirstName;
                existingBaker.LastName = request.LastName;
                existingBaker.EmailAddress = request.EmailAddress;
                existingBaker.Salary = request.Salary;
                existingBaker.IsFullTime = request.IsFullTime;
                existingBaker.RegisteredOn = request.RegisteredOn;

                // Save changes to the database
                repo.Save(existingBaker);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBaker(int id)
        {
            try
            {
                BakersRepository repo = new BakersRepository();

                Baker baker = repo.GetAll(n => n.Id == id).Find(i => i.Id == id);

                if (baker == null)
                {
                    return NotFound();
                }

                repo.Delete(baker);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("search/{searchWord}")]
        public IActionResult SearchBakersByFirstName(string searchWord)
        {
            try
            {
                BakersRepository repo = new BakersRepository();
                List<Baker> bakersSearchResult = repo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

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
            var response = new BakerResponse
            {
                Id = baker.Id,
                FirstName = baker.FirstName,
                LastName = baker.LastName,
                EmailAddress = baker.EmailAddress,
                Salary = baker.Salary,
                IsFullTime = baker.IsFullTime,
                RegisteredOn = baker.RegisteredOn,
                TotalOrders = baker.TotalOrders
            };

            return response;
        }
    }
}