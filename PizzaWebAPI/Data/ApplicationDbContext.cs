using PizzaWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzaWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Встановлюємо ForeignKey між Pizza та Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Pizza)
                .WithMany()  // Pizza не має списку замовлень
                .HasForeignKey(o => o.PizzaId);

            modelBuilder.Entity<Pizza>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }

}
