using Mobile.Models.ViewModels;

namespace Mobile.Web.Helpers
{
    public class APIHelper
    {
        private static APIHelper _instance;

        public static APIHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new APIHelper();
                return _instance;
            }
        }

        private APIHelper() { }

        public ApiViewModel GetApiResult(object references, string status, string statusMessage, int length)
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