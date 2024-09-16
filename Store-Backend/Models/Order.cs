namespace Store_Backend.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public required DateTime OrderState { get; set; }
        public required decimal TotalAmount { get; set; }
        public required int CustomerId { get; set; }
    }
}
