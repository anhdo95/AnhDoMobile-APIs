using Mobile.Common;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.ViewModels;
using Mobile.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mobile.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JsonResult> GetCommentsForProduct(int productId)
        {
            string status = Instances.ERROR_STATUS;
            string statusMessage = string.Empty;
            var comments = Enumerable.Empty<CommentViewModel>();
            try
            {
                comments = await _unitOfWork.CommentRepo.GetCommentsByProductId(productId);
                status = Instances.SUCCESS_STATUS;
            }
            catch (Exception ex)
            {
                statusMessage = ex.Message;
            }
            var results = APIHelper.Instance.GetApiResult(new
            {
                Comments = comments,
            }, status, statusMessage, comments.Count());
            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}