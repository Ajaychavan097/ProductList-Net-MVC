
using ProductList.Models;

namespace ProductList.Services
{
    public interface IProductServices
    {
        public List<Product> GetProductList();
        public string InsertProduct(Product product);
        public string UpdateProduct(Product product);          
        public string DeleteProduct(int ProductId);   

        
    }
}
