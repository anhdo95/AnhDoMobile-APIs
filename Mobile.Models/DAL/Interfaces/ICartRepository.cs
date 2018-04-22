using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<CartViewModel> GetCart(string cartId);
        Task AddToCart(Cart cart);
        Task RemoveFromCart(string cartId, int productId);
        Task ChangeQuantityFromCart(string cartId, int quantity);
    }
}
