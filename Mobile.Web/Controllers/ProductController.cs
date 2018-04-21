using Mobile.Common;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Mobile.Web.Helpers;
using Mobile.Models.Entities;

namespace Mobile.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResult> Index()
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var products = Enumerable.Empty<SearchProductViewModel>();
            try
            {
                products = await _unitOfWork.ProductRepo.GetAll();
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }

            var results = APIHelper.Instance.GetApiResult(new
            {
                Products = products
            }, status, statusMessage, products.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
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

            var results = APIHelper.Instance.GetApiResult(new {
                Products = products
            }, status, statusMessage, products.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetBestOutstanding()
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var products = Enumerable.Empty<SearchProductViewModel>();
            try
            {
                products = await _unitOfWork.ProductRepo.GetBestOutstanding(Instances.PRODUCT_BEST_OUTSTANDING_NUMBER_USED_TO_DISPLAY);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }

            var results = APIHelper.Instance.GetApiResult(new
            {
                Products = products
            }, status, statusMessage, products.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetBestSelling()
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var products = Enumerable.Empty<SearchProductViewModel>();
            try
            {
                products = await _unitOfWork.ProductRepo.GetBestSelling(Instances.PRODUCT_BEST_SELLING_NUMBER_USED_TO_DISPLAY);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }

            var results = APIHelper.Instance.GetApiResult(new
            {
                Products = products
            }, status, statusMessage, products.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetDetail(int id)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var product = new ProductDetailViewModel();
            var specification = new ProductSpecification();
            try
            {
                product = await _unitOfWork.ProductRepo.GetDetail(id);
                specification = await _unitOfWork.SpecificationRepo.GetByIdAsync(id);
                specification.Product = null; // Avoid a circular reference to product instance
                
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new {
                Product = product,
                Specification = specification
            }, status, statusMessage, 2);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}