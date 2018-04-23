using System.Web.Mvc;
using System.Web.Routing;

namespace Mobile.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Product Routes
            routes.MapRoute(
                name: "All Product",
                url: "Products",
                defaults: new { controller = "Product", action = "Index" }
            );

            routes.MapRoute(
                name: "Search Product",
                url: "SearchProducts",
                defaults: new { controller = "Product", action = "Search" }
            );

            routes.MapRoute(
                name: "Best Outstanding",
                url: "{controller}/BestOutstanding",
                defaults: new { controller = "Product", action = "GetBestOutstanding" }
            );

            routes.MapRoute(
                name: "Best Selling",
                url: "{controller}/BestSelling",
                defaults: new { controller = "Product", action = "GetBestSelling" }
            );

            routes.MapRoute(
                name: "Product Detail",
                url: "ProductDetail/{id}",
                defaults: new { controller = "Product", action = "GetDetail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product Related",
                url: "{controller}/Related/{id}",
                defaults: new { controller = "Product", action = "GetRelated", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Product Categories",
                url: "Categories",
                defaults: new { controller = "Product", action = "GetCategories"}
            );
            #endregion

            #region Order Routes
            routes.MapRoute(
                name: "View Order",
                url: "ViewOrder",
                defaults: new { controller = "Order", action = "Index" }
            );

            routes.MapRoute(
                name: "Get CartId",
                url: "GetCartId",
                defaults: new { controller = "Order", action = "GetCartId" }
            );

            routes.MapRoute(
                name: "Add To Cart",
                url: "AddToCart",
                defaults: new { controller = "Order", action = "AddToCart" }
            );

            routes.MapRoute(
                name: "Remove From Cart",
                url: "RemoveFromCart",
                defaults: new { controller = "Order", action = "RemoveFromCart" }
            );

            routes.MapRoute(
                name: "Change Quantity From Cart",
                url: "ChangeQuantityFromCart",
                defaults: new { controller = "Order", action = "ChangeQuantityFromCart" }
            );

            routes.MapRoute(
                name: "Order Processing",
                url: "OrderProcessing",
                defaults: new { controller = "Order", action = "OrderProcess" }
            );
            #endregion

            routes.MapRoute(
                name: "Comments For Product",
                url: "CommentsForProduct",
                defaults: new { controller = "Comment", action = "GetCommentsForProduct" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
