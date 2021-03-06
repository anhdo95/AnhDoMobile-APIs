﻿using Mobile.Common;
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
        public async Task<ActionResult> RemoveFromCart(int recordId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            try
            {
                await _unitOfWork.CartRepo.RemoveFromCart(recordId);
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
        public async Task<ActionResult> ChangeQuantityFromCart(int recordId, int newQuantity)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            try
            {
                await _unitOfWork.CartRepo.ChangeQuantityFromCart(recordId, newQuantity);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new{
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

        public JsonResult GetProvinces()
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var provinces = Enumerable.Empty<ProvinceViewModel>();
            try
            {
                provinces =  _unitOfWork.OrderRepo.GetProvinces();
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new {
                Provinces = provinces
            }, status, statusMessage, provinces.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDistricts(int provinceId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var districts = Enumerable.Empty<DistrictViewModel>();
            try
            {
                districts = _unitOfWork.OrderRepo.GetDistricts(provinceId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
                Districts = districts
            }, status, statusMessage, districts.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}