using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System;

namespace Mobile.Models.DAL.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MobileDbContext context) : base(context)
        {
        }

        public Task<bool> IsCustomerExist(string fullName, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
