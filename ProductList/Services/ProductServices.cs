using Dapper;
using ProductList.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProductList.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IConfiguration _configuration;

        public ProductServices(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DBConnection");
            provideName = "System.Data.SqlClient";

        }

        public string ConnectionString { get; }
        public string provideName { get; }

        public IDbConnection connection
        {
            get { return new SqlConnection(ConnectionString); }
        }
        public string DeleteProduct(int ProductId)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var prod = dbConnection.Query<Product>("DeleteProduct",
                        new { 
                                 ProductId = ProductId
                            }, 
                            commandType: CommandType.StoredProcedure).ToList();

                    if (prod != null && prod.FirstOrDefault().Response == "Delete Successfully")
                    {
                        result = "Delete Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public string InsertProduct(Product product)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var prod = dbConnection.Query<Product>("InsertProduct", 
                        new {   ProductName= product.ProductName,
                                ProductPrice=product.ProductPrice 
                            }, 
                            commandType: CommandType.StoredProcedure).ToList();

                    if (prod != null && prod.FirstOrDefault().Response == "Save Successfully")
                    {
                        result = "Save Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public List<Product> GetProductList()
        {
            List<Product> products = new List<Product>();
            try
            {
                using(IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    products = dbConnection.Query<Product>("GetProductList", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return products;
                }
            }
            catch(Exception ex)
            {
                string errorMsg = ex.Message;
                return products;
            }
        }

        public string UpdateProduct(Product product)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = connection)
                {
                    dbConnection.Open();
                    var prod = dbConnection.Query<Product>("UpdateProduct",
                        new {   ProductName = product.ProductName, 
                                ProductPrice = product.ProductPrice, 
                                ProductId = product.ProductId
                            },
                            commandType: CommandType.StoredProcedure).ToList();

                    if (prod != null && prod.FirstOrDefault().Response == "Update Successfully")
                    {
                        result = "Update Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
    }
}
