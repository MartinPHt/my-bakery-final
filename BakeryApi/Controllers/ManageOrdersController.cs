using BakeryApi.Repositories;
using Common.CommConstants;
using Common.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageOrdersController : ControllerBase
    {
        private readonly OrdersRepository _ordersRepo;
        private readonly CustomersRepository _customersRepo;
        private readonly BakersRepository _bakersRepo;

        public ManageOrdersController()
        {
            _ordersRepo = new OrdersRepository();
            _customersRepo = new CustomersRepository();
            _bakersRepo = new BakersRepository();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = new Order(request.Details, request.Quantity, request.Tip, request.Total, request.IsExpress, request.PlacedOn, request.Customer_ID, request.Baker_ID);
                _ordersRepo.Save(order);

                var response = GenerateResponse(order);
                return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Retrieve by ID
        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {
            try
            {
                var order = _ordersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                var response = GenerateResponse(order);
                return Ok(response);
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
                var allOrders = _ordersRepo.GetAll(i => true);
                var response = allOrders.Select(order => GenerateResponse(order)).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Update
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] UpdateOrderRequest request)
        {
            try
            {
                var order = _ordersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                order.Details = request.Details;
                order.Quantity = request.Quantity;
                order.Tip = request.Tip;
                order.Total = request.Total;
                order.IsExpress = request.IsExpress;
                order.PlacedOn = request.PlacedOn;
                order.Customer_ID = request.Customer_ID;
                order.Baker_ID = request.Baker_ID;

                _ordersRepo.Save(order);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var order = _ordersRepo.GetAll(n => n.Id == id).Find(i => i.Id == id);
                if (order == null)
                {
                    return NotFound();
                }

                _ordersRepo.Delete(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpGet("search/{filter}/{searchWord}")]
        public IActionResult SearchOrdersByDetails(string filter, string searchWord)
        {
            try
            {
                List<Order> ordersSearchResult;
                if (filter == "Customer")
                    ordersSearchResult = _ordersRepo.GetAll(n => n.Customer.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()) || n.Customer.LastName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

                else if (filter == "Baker")
                    ordersSearchResult = _ordersRepo.GetAll(n => n.Baker.FirstName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()) || n.Baker.LastName.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

                else
                    ordersSearchResult = _ordersRepo.GetAll(n => n.Details.ToUpper().Replace(" ", "").Contains(searchWord.ToUpper()));

                var response = ordersSearchResult.Select(order => GenerateResponse(order)).ToList();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private OrderResponse GenerateResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                Details = order.Details,
                Tip = order.Tip,
                Total = order.Total,
                IsExpress = order.IsExpress,
                PlacedOn = order.PlacedOn,
                Quantity = order.Quantity,
                Customer = _customersRepo.GetAll(n => n.Id == order.Customer_ID).Find(i => i.Id == order.Customer_ID),
                Baker = _bakersRepo.GetAll(n => n.Id == order.Baker_ID).Find(i => i.Id == order.Baker_ID)
            };
        }
    }
}
