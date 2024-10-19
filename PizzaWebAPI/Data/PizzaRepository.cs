using Microsoft.EntityFrameworkCore;
using PizzaWebAPI.Models;

namespace PizzaWebAPI.Data
{
    public class PizzaRepository : IRepository<Pizza>
    {
        private readonly ApplicationDbContext _context;

        public PizzaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pizza>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Pizzas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Pizza> GetByIdAsync(int id)
        {
            return await _context.Pizzas.FindAsync(id);
        }

        public async Task AddAsync(Pizza entity)
        {
            await _context.Pizzas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pizza entity)
        {
            _context.Pizzas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pizza = await GetByIdAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
                await _context.SaveChangesAsync();
            }
        }
    }

}
