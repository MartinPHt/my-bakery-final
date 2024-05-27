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
        private readonly BakersRepository _bakersRepo;
        private readonly OrdersRepository _ordersRepo;
        public ManageBakersController()
        {
            _bakersRepo = new BakersRepository();
            _ordersRepo = new OrdersRepository();
        }

        [HttpPost]
        public IActionResult CreateBaker([FromBody] CreateBakerRequest request)
        {
            try
            {
                // Save to database
                Baker baker = new Baker(request.FirstName, request.LastName, request.EmailAddress, request.Salary, request.IsFullTime, request.RegisteredOn);
                _bakersRepo.Save(baker);

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
                Baker baker = _bakersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);

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
                List<Baker> allBakers = _bakersRepo.GetAll(i => true);
                List<Order> allOrders = _ordersRepo.GetAll(i => true);

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
        public IActionResult UpdateBaker(int id, [FromBody] UpdateBakerRequest request)
        {
            try
            {
                // Find the existing baker object
                var existingBaker = _bakersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
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
                _bakersRepo.Save(existingBaker);
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
                Baker baker = _bakersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (baker == null)
                    return NotFound();

                _bakersRepo.Delete(baker);
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
                List<Baker> bakersSearchResult = _bakersRepo.GetAll(n => n.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

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