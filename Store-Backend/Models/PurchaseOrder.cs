namespace Store_Backend.Models
{
    public class PurchaseOrderDetail
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required int CategoryId { get; set; }
        public int Quantity { get; set; }
    }
    public class PurchaseOrder
    {
        public List<PurchaseOrderDetail> Items { get; set; }
        public int Total { get; set; }
        public int TotalItems { get; set; }
    }
}
