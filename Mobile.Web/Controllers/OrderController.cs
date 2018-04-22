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

        public async Task<JsonResult> Index()
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var cart = _unitOfWork.CartRepo.GetCart(this);
            var result = new CartViewModel();
            try
            {
                result.CartItems = await cart.GetCartItems();
                result.CartTotalPrice = await cart.GetTotalPrice();
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

        public async Task<ActionResult> AddToCart(int productId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var cart = _unitOfWork.CartRepo.GetCart(this);
            try
            {
                await cart.AddToCart(productId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
            }, status, statusMessage);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> RemoveFromCart(int productId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var cart = _unitOfWork.CartRepo.GetCart(this);
            try
            {
                await cart.RemoveFromCart(productId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
            }, status, statusMessage);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ChangeQuantityFromCart(int productId, int newQuantity)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var cart = _unitOfWork.CartRepo.GetCart(this);
            try
            {
                await cart.ChangeQuantityFromCart(productId, newQuantity);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
            }, status, statusMessage);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> OrderProcess(string model)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var cart = _unitOfWork.CartRepo.GetCart(this);
            var result = new OrderCompleteViewModel();
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Customer customer = serializer.Deserialize<Customer>(model);
                int orderId = await _unitOfWork.OrderRepo.CheckOut(cart, customer);
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
            }, status, statusMessage);
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}