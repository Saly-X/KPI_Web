using System.ComponentModel.DataAnnotations;

namespace PizzaWebAPI.DTOs
{
    public class OrderCreateDto
    {
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
    }

}
