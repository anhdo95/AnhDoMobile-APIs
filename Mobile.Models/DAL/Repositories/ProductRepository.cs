using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;

namespace Mobile.Models.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MobileDbContext context) : base(context)
        {
        }
    }
}
