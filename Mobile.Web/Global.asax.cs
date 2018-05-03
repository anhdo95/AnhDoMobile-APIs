using System.Web.Mvc;
using System.Web.Routing;

namespace Mobile.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            AutoMapper.Mapper.Initialize(cfg => cfg.AddProfile<Models.Mapping.MappingProfile>());
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
