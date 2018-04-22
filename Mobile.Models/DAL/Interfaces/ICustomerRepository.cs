using Mobile.Models.Entities;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> IsCustomerExist(string fullName, string phoneNumber);
    }
}
