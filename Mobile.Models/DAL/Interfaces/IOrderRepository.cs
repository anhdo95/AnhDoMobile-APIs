using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<OrderCompleteViewModel> Complete(int id);
        Task<int> CheckOut(string cartId, Customer customer);
    }
}
