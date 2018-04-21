using Mobile.Models.DAL.Interfaces;
using Mobile.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mobile.Common;
using Mobile.Models.Entities;

namespace Mobile.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResult> Index()
        {
            string status;
            string statusMessage = string.Empty;
            Task<IEnumerable<Menu>> menus = null;
            try
            {
                menus = GetMenus();
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                status = Instances.ERROR_STATUS;
                statusMessage = ex.Message;
            }
            
            var results = new ApiViewModel {
                References = new {
                    Menus = await menus
                },
                Status = status,
                StatusMessage = statusMessage,
                Length = menus.Result.Count()
            };
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<Menu>> GetMenus()
        {
            return await _unitOfWork.MenuRepo.GetAsync(
                            filter: m => m.Status,
                            orderBy: list => list.OrderBy(m => m.DisplayOrder)
                            );
        }
    }
}