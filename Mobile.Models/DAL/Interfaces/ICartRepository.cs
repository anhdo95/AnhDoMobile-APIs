using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<CartItemViewModel>> GetCartItems(string cartId);
        Task<decimal> GetTotalPrice(string cartId);
        Task AddToCart(int productId, string cartId);
        Task RemoveFromCart(int productId, string cartId);
        Task ChangeQuantityFromCart(int productId, int newQuantity, string cartId);
        Task EmptyCart(string cartId);
    }
}
