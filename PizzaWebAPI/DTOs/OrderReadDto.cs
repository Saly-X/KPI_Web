namespace PizzaWebAPI.DTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }

}
