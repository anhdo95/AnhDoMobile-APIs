using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System;

namespace Mobile.Models.DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MobileDbContext context) : base(context)
        {
        }

        public Task<int> CheckOut(string cartId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderCompleteViewModel> Complete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
