using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaWebAPI.Data;
using PizzaWebAPI.DTOs;
using PizzaWebAPI.Models;

namespace PizzaWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context, IRepository<Order> orderRepository, IRepository<Pizza> pizzaRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
        }

        // GET: api/orders?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrders(int pageNumber = 1, int pageSize = 10)
        {
            var orders = await _orderRepository.GetAllAsync(pageNumber, pageSize);

            Console.WriteLine("aboba\n" + orders.ToList()[0].Pizza);

            var ordersDtos = orders.Select(o => new OrderReadDto
            {
                Id = o.Id,
                PizzaId = o.PizzaId,
                PizzaName = o.Pizza?.Name ?? "Unknown Pizza",
                Quantity = o.Quantity,
                OrderDate = o.OrderDate,
                Status = o.Status
            });

            return Ok(ordersDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return NotFound();

            return Ok(new OrderReadDto
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                PizzaName = order.Pizza.Name,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate,
                Status = order.Status
            });
        }

        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> PostOrder(OrderCreateDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pizza = await _pizzaRepository.GetByIdAsync(orderDto.PizzaId);

            if (pizza == null)
            {
                return BadRequest("Pizza not found.");
            }

            // Створюємо нове замовлення
            var order = new Order
            {
                PizzaId = orderDto.PizzaId,
                Quantity = orderDto.Quantity,
                OrderDate = DateTime.Now,
                Status = "New"
            };

            // Додаємо замовлення через репозиторій
            await _orderRepository.AddAsync(order);

            // Створюємо DTO для відповіді
            var orderReadDto = new OrderReadDto
            {
                Id = order.Id,
                PizzaId = order.PizzaId,
                PizzaName = pizza.Name,
                Quantity = order.Quantity,
                OrderDate = order.OrderDate,
                Status = order.Status
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderCreateDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.PizzaId = orderDto.PizzaId;
            order.Quantity = orderDto.Quantity;
            order.Status = "Updated";

            await _orderRepository.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null) return NotFound();

            await _orderRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
