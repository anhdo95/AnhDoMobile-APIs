using Mobile.Common;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using Mobile.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Mobile.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public JsonResult GetCartId()
        {
            return Json(new
            {
                CartId = Guid.NewGuid().ToString()
            }, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> Index(string cartId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var result = new CartViewModel();
            try
            {
                result.CartItems = await _unitOfWork.CartRepo.GetCartItems(cartId);
                result.CartTotalPrice = await _unitOfWork.CartRepo.GetTotalPrice(cartId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
                Results = result,
            }, status, statusMessage, result.CartItems.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart(int productId, string cartId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            try
            {
                await _unitOfWork.CartRepo.AddToCart(productId, cartId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
            }, status, statusMessage);
            return Json(results);
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(int productId, string cartId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            try
            {
                await _unitOfWork.CartRepo.RemoveFromCart(productId, cartId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
            }, status, statusMessage);
            return Json(results);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeQuantityFromCart(int productId, int newQuantity, string cartId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            try
            {
                await _unitOfWork.CartRepo.ChangeQuantityFromCart(productId, newQuantity, cartId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
            }, status, statusMessage);
            return Json(results);
        }

        [HttpPost]
        public async Task<JsonResult> OrderProcess(string model, string cartId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var result = new OrderCompleteViewModel();
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Customer customer = serializer.Deserialize<Customer>(model);
                int orderId = await _unitOfWork.OrderRepo.CheckOut(cartId, customer);
                result = await _unitOfWork.OrderRepo.Complete(orderId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
                Result = result
            }, status, statusMessage, result.OrderItems.Count());
            return Json(results);
        }
    }
}