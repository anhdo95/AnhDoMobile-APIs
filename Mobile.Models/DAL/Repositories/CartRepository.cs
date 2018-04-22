using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Linq;
using Mobile.Models.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
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

        public string ShoppingCartId { get; private set; }
        private const string CART_SESSIONKEY = "CartId";

        public CartRepository GetCart(Controller controller)
        {
            var cart = new CartRepository(_context, _unitOfWork);
            cart.ShoppingCartId = cart.GetCartId(controller.HttpContext);
            return cart;
        }

        public async Task<IEnumerable<CartItemViewModel>> GetCartItems()
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
                filter: c => c.CartId == ShoppingCartId,
                orderBy: list => list.OrderBy(c => c.CreatedDate));
        }

        public async Task<decimal> GetTotalPrice()
        {
            decimal? total = await (from c in _dbSet
                        where c.CartId == ShoppingCartId
                        select (decimal?)c.Price * c.Quantity).SumAsync();

            return total ?? 0;
        }

        public async Task AddToCart(int productId)
        {
            var cartItem = await _dbSet.SingleOrDefaultAsync(c => c.CartId == ShoppingCartId && c.ProductId == productId);

            if (cartItem == null)
            {
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(productId);
                decimal price = product.PromotionPrice ?? product.Price;
                Insert(new Cart
                {
                    CartId = ShoppingCartId,
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

        public async Task ChangeQuantityFromCart(int productId, int newQuantity)
        {
            var cartItem = await _dbSet.SingleAsync(c => c.CartId == ShoppingCartId && c.ProductId == productId);
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

        public async Task RemoveFromCart(int productId)
        {
            var cartItem = await _dbSet.SingleAsync(c => c.CartId == ShoppingCartId && c.ProductId == productId);
            await DeleteAsync(cartItem.RecordId);
            await _unitOfWork.SaveAsync();
        }

        /// <summary>
        /// This method is only changed entities state to deleted and not calls SaveChanges method.
        /// </summary>
        /// <returns></returns>
        public async Task EmptyCart()
        {
            var cartItems = await GetAsync(filter: c => c.CartId == ShoppingCartId);
            if (cartItems.Count() > 0)
            {
                foreach (var cart in cartItems)
                {
                    Delete(cart);
                }
            }
        }

        private string GetCartId(HttpContextBase context)
        {
            if (!context.Request.Cookies.AllKeys.Contains(CART_SESSIONKEY))
            {
                Guid tempCartId = Guid.NewGuid();
                HttpCookie cookie = new HttpCookie(CART_SESSIONKEY);
                cookie.Value = tempCartId.ToString();
                cookie.Expires = DateTime.Now.AddDays(7);
                context.Response.Cookies.Add(cookie);
            }
            return context.Request.Cookies[CART_SESSIONKEY].Value.ToString();
        }
    }
}
