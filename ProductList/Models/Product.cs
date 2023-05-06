namespace ProductList.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int CreatedBy { get; internal set; }
        public string Response { get; internal set; }
    }
}
