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
                products = await SearchByKeyword(keyword, Instances.PRODUCT_SEARCH_NUMBER_USED_TO_DISPLAY);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }

            var results = GetApi(new {
                Products = products
            }, status, statusMessage, products.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<SearchProductViewModel>> SearchByKeyword(string keyword, int? topNumber = null)
        {
            return await _unitOfWork.ProductRepo.SearchByKeyword(keyword, topNumber);
        }

        private ApiViewModel GetApi(object references, string status, string statusMessage, int length)
        {
            return new ApiViewModel
            {
                References = references,
                Status = status,
                StatusMessage = statusMessage,
                Length = length
            };
        }
    }
}