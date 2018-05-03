using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System;
using Mobile.Common.Enums;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;

namespace Mobile.Models.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string xmlPath = HttpContext.Current.Server.MapPath(@"~/App_Data/Provinces_Data.xml");

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
            var order = await SelectByIdAsync<OrderCompleteViewModel>(id);

            order.OrderItems = await _unitOfWork.OrderDetailRepo
                .Select<CompleteProductViewModel>(filter: od => od.OrderId == id);

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

        public IEnumerable<DistrictViewModel> GetDistricts(int provinceId)
        {
            var xmlDoc = XDocument.Load(xmlPath);
            var xElements = xmlDoc.Element("Root").Elements("Item").Single(x => x.Attribute("id").Value == provinceId.ToString());

            foreach (var item in xElements.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                yield return new DistrictViewModel {
                    Id = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("value").Value,
                    ProvinceId = provinceId
                };
            }
        }

        public IEnumerable<ProvinceViewModel> GetProvinces()
        {
            var xmlDoc = XDocument.Load(xmlPath);
            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");

            foreach (var item in xElements)
            {
                yield return new ProvinceViewModel {
                    Id = int.Parse(item.Attribute("id").Value),
                    Name = item.Attribute("value").Value
                };
            }
        }
    }
}
