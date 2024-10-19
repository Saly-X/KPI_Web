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
    public class PizzaController : ControllerBase
    {
        private readonly IRepository<Pizza> _pizzaRepository;
        private readonly ApplicationDbContext _context;

        public PizzaController(ApplicationDbContext context, IRepository<Pizza> pizzaRepository)
        {
            _context = context;
            _pizzaRepository = pizzaRepository;
        }

        // GET: api/pizzas?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaReadDto>>> GetPizzas(int pageNumber = 1, int pageSize = 10)
        {
            var pizzas = await _pizzaRepository.GetAllAsync(pageNumber, pageSize);

            var pizzaDtos = pizzas.Select(p => new PizzaReadDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description
            });

            return Ok(pizzaDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaReadDto>> GetPizza(int id)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(id);
            if (pizza == null) return NotFound();

            return Ok(new PizzaReadDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Price = pizza.Price,
                Description = pizza.Description
            });
        }

        [HttpPost]
        public async Task<ActionResult<PizzaReadDto>> PostPizza(PizzaCreateDto pizzaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pizza = new Pizza
            {
                Name = pizzaDto.Name,
                Price = pizzaDto.Price,
                Description = pizzaDto.Description
            };

            await _pizzaRepository.AddAsync(pizza);
            return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(int id, PizzaCreateDto pizzaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pizza = await _pizzaRepository.GetByIdAsync(id);
            if (pizza == null) return NotFound();

            pizza.Name = pizzaDto.Name;
            pizza.Price = pizzaDto.Price;
            pizza.Description = pizzaDto.Description;

            await _pizzaRepository.UpdateAsync(pizza);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _pizzaRepository.GetByIdAsync(id);
            if (pizza == null) return NotFound();

            await _pizzaRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
