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
        public ManageOrdersController()
        {

        }

        [HttpPost(Endpoints.CreateOrderEndPoint)]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                //Save to database
                OrdersRepository ordersRepo = new OrdersRepository();
                Order order = new Order(request.Details, request.Quantity, request.Tip, request.Total, request.IsExpress, request.PlacedOn, request.Customer_ID, request.Baker_ID);
                ordersRepo.Save(order);

                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.GetOrderEndPoint)]
        public IActionResult Get(GetOrderRequest request)
        {
            try
            {
                //Retrieve from database
                OrdersRepository repo = new OrdersRepository();
                Order order = repo.GetAll(n => n.Id == request.Id).Find(i => i.Id == request.Id);

                //generate response
                var response = GenerateResponse(order, new CustomersRepository(), new BakersRepository());

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.GetAllOrdersEndPoint)]
        public IActionResult GetAll()
        {
            try
            {
                OrdersRepository ordersRepo = new OrdersRepository();
                List<Order> allOrders = ordersRepo.GetAll(i => true);
                var response = allOrders.Select(order => GenerateResponse(order, new CustomersRepository(), new BakersRepository())).ToList();

                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.UpdateOrderEndPoint)]
        public IActionResult Update(UpdateOrderRequest request)
        {
            try
            {
                OrdersRepository repo = new OrdersRepository();
                repo.Save(GenerateOrder(request));

                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.DeleteOrderEndPoint)]
        public IActionResult Delete(DeleteOrderRequest request)
        {
            try
            {
                OrdersRepository repo = new OrdersRepository();

                Order order = new Order();
                order.Id = request.Id;

                repo.Delete(order);
                return new JsonResult(Ok());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        [HttpPost(Endpoints.SearchOrdersByDetailsEndPoint)]
        public IActionResult Srearch(SearchOrdersByDetailsRequest request)
        {
            try
            {
                OrdersRepository repo = new OrdersRepository();
                List<Order> ordersSearchResult = repo.GetAll(n => n.Details.ToUpper().Replace(" ", "").Contains(request.Details));
                var response = ordersSearchResult.Select(order => GenerateResponse(order, new CustomersRepository(), new BakersRepository())).ToList();
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while processing your request.", details = ex.Message });
            }
        }

        private OrderResponse GenerateResponse(Order order, CustomersRepository customersRepo, BakersRepository bakersRepo)
        {
            var response = new OrderResponse();
            response.Id = order.Id;
            response.Details = order.Details;
            response.Tip = order.Tip;
            response.Total = order.Total;
            response.IsExpress = order.IsExpress;
            response.PlacedOn = order.PlacedOn;
            response.Quantity = order.Quantity;

            //Foreign Keys
            response.Customer = customersRepo.GetAll(n => n.Id == order.Customer_ID).Find(i => i.Id == order.Customer_ID);
            response.Baker = bakersRepo.GetAll(n => n.Id == order.Baker_ID).Find(i => i.Id == order.Baker_ID);

            return response;
        }

        private Order GenerateOrder(UpdateOrderRequest request)
        {
            return new Order()
            {
                Id = request.Id,
                Details = request.Details,
                Quantity = request.Quantity,
                Tip = request.Tip,
                Total = request.Total,
                IsExpress = request.IsExpress,
                PlacedOn = request.PlacedOn,
                Customer_ID = request.Customer_ID,
                Baker_ID = request.Baker_ID,
            };
        }
    }
}
