namespace Store_Backend.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public required int OrderId { get; set; }
        public required int ProductId { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
    }
}
