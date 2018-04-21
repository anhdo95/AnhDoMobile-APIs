using Mobile.Models.DAL.Interfaces;
using Mobile.Models.DAL.Repositories;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Mobile.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}