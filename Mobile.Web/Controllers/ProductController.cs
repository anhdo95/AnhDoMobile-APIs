using Mobile.Common;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

namespace Mobile.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResult> Search(string keyword)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var products = Enumerable.Empty<SearchProductViewModel>();
            try
            {
                products = await _unitOfWork.ProductRepo.SearchByKeyword(keyword, Instances.PRODUCT_SEARCH_NUMBER_USED_TO_DISPLAY);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }

            var results = _unitOfWork.GetApi(new {
                Products = products
            }, status, statusMessage, products.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}