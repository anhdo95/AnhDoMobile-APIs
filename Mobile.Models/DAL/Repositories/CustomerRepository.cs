using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Data.Entity;

namespace Mobile.Models.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MobileDbContext context) : base(context)
        {
        }

        public async Task<int> IsCustomerExist(bool gender, string fullName, string phoneNumber)
        {
            var customer = await _dbSet.SingleOrDefaultAsync(
                c => c.Gender == gender && 
                c.FullName == fullName && 
                c.PhoneNumber == phoneNumber);
            return customer == null ? -1 : customer.Id;
        }
    }
}
