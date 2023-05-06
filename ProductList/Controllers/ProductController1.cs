using Microsoft.AspNetCore.Mvc;
using ProductList.Models;
using ProductList.Services;

namespace ProductList.Controllers
{
    public class StudentController1 : Controller
    {
        private readonly IConfiguration _configuration; 
        private readonly IProductServices _productServices;

        public StudentController1(IConfiguration configuration, IProductServices productServices)
        {
            _configuration = configuration;
            _productServices = productServices;
        }

        public IProductServices Get_productServices()
        {
            return _productServices;
        }

        public IActionResult Index()
        {
            ProductVM model = new ProductVM();
            model.ProductList = _productServices.GetProductList().ToList();
            return View();
        }

        [HttpPost]

        public IActionResult AddUpdateProduct(Product product)
        {
            ProductVM model = new ProductVM();

            product.CreatedBy = 1;
            string url = Request.Headers["Referer"].ToString();

            string result=string.Empty;
            if (product.ProductId>0)
            {
                result=_productServices.UpdateProduct(product);
            }
            else
            {
                result=_productServices.InsertProduct(product); 
            }

            if(result == "Save Successfully")
            {
                TempData["SuccessMsg"] = "Save Successfully";
                return Redirect(url);
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return Redirect(url);
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteProduct(int ProductId)
        {
            string url = Request.Headers["Referer"].ToString();
            string result = _productServices.DeleteProduct(ProductId);
            if(result=="Delete Successfully")
            {
                return Json(new { message = "Delete Successfully" });
            }
            else
            {
                return Json(new { message = "Error Occured" });
            }
        }
    }
}


