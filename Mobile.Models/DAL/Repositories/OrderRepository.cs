using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System;
using Mobile.Common.Enums;
using System.Data.Entity;
using System.Linq;

namespace Mobile.Models.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderRepository(MobileDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CheckOut(string cartId, Customer customer)
        {
            int customerId = await _unitOfWork.CustomerRepo.IsCustomerExist(customer.Gender, customer.FullName, customer.PhoneNumber);
            if (customerId == -1)
            {
                _unitOfWork.CustomerRepo.Insert(customer);
            }
            else
            {
                customer.Id = customerId;
            }
            int orderId = await CreateOrderByCustomer(cartId, customer);
            return orderId;
        }

        public async Task<OrderCompleteViewModel> Complete(int id)
        {
            var order = await (from o in _dbSet
                               where o.Id == id
                               select new OrderCompleteViewModel {
                                   OrderId = o.Id,
                                   ShipName = o.ShipName,
                                   ShipGender = o.ShipGender,
                                   ShipAddress = o.ShipAddress,
                                   ShipPhone = o.ShipMobile,
                                   OrderTotal = o.Total
                               }).SingleOrDefaultAsync();

            order.OrderItems = await _unitOfWork.OrderDetailRepo.Select(
                od => new OrderDetailViewModel {
                    ProductId = od.ProductId,
                    Price = od.Price,
                    Quantity = od.Quantity
                }, filter: od => od.OrderId == id);

            return order;
        }

        private async Task<int> CreateOrderByCustomer(string cartId, Customer customer)
        {
            var order = new Order
            {
                ShipName = customer.FullName,
                ShipGender = customer.Gender,
                ShipMobile = customer.PhoneNumber,
                ShipAddress = customer.Address,
                Status = OrderStatus.Pending,
                Total = await _unitOfWork.CartRepo.GetTotalPrice(cartId),
                CreatedDate = DateTime.Now,
                CustomerId = customer.Id
            };
            Insert(order);
            foreach (var item in await _unitOfWork.CartRepo.GetCartItems(cartId))
            {
                _unitOfWork.OrderDetailRepo.Insert(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
            await _unitOfWork.CartRepo.EmptyCart(cartId);
            await _unitOfWork.SaveAsync();
            return order.Id;
        }
    }
}
