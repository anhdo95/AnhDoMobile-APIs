using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Linq;
using Mobile.Models.ViewModels;
using System;

namespace Mobile.Models.DAL.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(MobileDbContext context) : base(context)
        {
        }

        public Task<CartViewModel> GetCart(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task AddToCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task ChangeQuantityFromCart(string cartId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromCart(string cartId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
