using System.ComponentModel.DataAnnotations;

namespace PizzaWebAPI.DTOs 
{
    public class PizzaCreateDto
    {
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

}