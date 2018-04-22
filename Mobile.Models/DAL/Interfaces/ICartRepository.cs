using Mobile.Models.DAL.Repositories;
using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mobile.Models.DAL.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        CartRepository GetCart(Controller controller);
        Task<IEnumerable<CartItemViewModel>> GetCartItems();
        Task<decimal> GetTotalPrice();
        Task AddToCart(int productId);
        Task RemoveFromCart(int productId);
        Task ChangeQuantityFromCart(int productId, int newQuantity);
        Task EmptyCart();
    }
}
