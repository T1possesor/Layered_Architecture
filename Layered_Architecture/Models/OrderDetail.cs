namespace Layered_Architecture.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = "";
        public string ProductName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}