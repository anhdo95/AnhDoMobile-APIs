using Mobile.Models.Entities;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        /// <returns>Customer's id</returns>
        Task<int> IsCustomerExist(bool gender, string fullName, string phoneNumber);
    }
}
