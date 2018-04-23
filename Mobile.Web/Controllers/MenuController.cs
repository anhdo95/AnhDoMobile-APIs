using Mobile.Models.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mobile.Common;
using Mobile.Models.Entities;
using Mobile.Web.Helpers;

namespace Mobile.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [OutputCache(Duration = 86400, VaryByParam = "None")]
        public async Task<JsonResult> Index()
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var menus = Enumerable.Empty<Menu>();
            try
            {
                menus = await GetMenus();
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }

            var results = APIHelper.Instance.GetApiResult(new {
                Menus = menus
            }, status, statusMessage, menus.Count());
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