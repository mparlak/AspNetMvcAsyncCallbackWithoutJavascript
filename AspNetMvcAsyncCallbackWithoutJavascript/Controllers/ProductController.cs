using AspNetMvcAsyncCallbackWithoutJavascript.Common;
using AspNetMvcAsyncCallbackWithoutJavascript.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvcAsyncCallbackWithoutJavascript.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductClient productClient;

        public ProductController()
        {
            var apiClient = new ApiClient();
            productClient = new ProductClient(apiClient);
        }

        public ProductController(IProductClient productClient)
        {
            this.productClient = productClient;
        }

        public async Task<ActionResult> GetProduct(int id)
        {
            var product = await productClient.GetProduct(id);
            var model = new ProductViewModel();
            return View(model);
        }
    }
}