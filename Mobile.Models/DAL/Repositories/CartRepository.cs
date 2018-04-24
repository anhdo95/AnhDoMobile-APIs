using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Linq;
using Mobile.Models.ViewModels;
using System;
using System.Data.Entity;

namespace Mobile.Models.DAL.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartRepository(MobileDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartItems(string cartId)
        {
            return await Select(
                c => new CartItemViewModel
                {
                    RecordId = c.RecordId,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    ProductMetaTitle = c.Product.MetaTitle,
                    ProductImage = c.Product.Image,
                    Price = c.Price,
                    Quantity = c.Quantity
                },
                filter: c => c.CartId == cartId,
                orderBy: list => list.OrderBy(c => c.CreatedDate));
        }

        public async Task<decimal> GetTotalPrice(string cartId)
        {
            decimal? total = await (from c in _dbSet
                        where c.CartId == cartId
                        select (decimal?)c.Price * c.Quantity).SumAsync();

            return total ?? 0;
        }

        public async Task AddToCart(int productId, string cartId)
        {
            var cartItem = await _dbSet.SingleOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId);

            if (cartItem == null)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(productId);
                decimal price = product.PromotionPrice ?? product.Price;
                Insert(new Cart
                {
                    CartId = cartId,
                    ProductId = productId,
                    Price = price,
                    Quantity = 1,
                    CreatedDate = DateTime.Now
                });
            }
            else
            {
                cartItem.Quantity += 1;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task ChangeQuantityFromCart(int recordId, int newQuantity)
        {
            var cartItem = await GetByIdAsync(recordId);
            if (newQuantity <= 0)
            {
                await DeleteAsync(cartItem.RecordId);
            }
            else
            {
                cartItem.Quantity = newQuantity;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveFromCart(int recordId)
        {
            var cartItem = await GetByIdAsync(recordId);
            await DeleteAsync(cartItem.RecordId);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// This method is only changed entities state to deleted and not calls SaveChanges method.
        /// </summary>
        /// <returns></returns>
        public async Task EmptyCart(string cartId)
        {
            var cartItems = await GetAsync(filter: c => c.CartId == cartId);
            if (cartItems.Count() > 0)
            {
                foreach (var cart in cartItems)
                {
                    Delete(cart);
                }
            }
        }
    }
}
