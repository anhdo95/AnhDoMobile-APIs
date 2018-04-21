using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Linq;
using Mobile.Models.ViewModels;
using System.Data.Entity;

namespace Mobile.Models.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MobileDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SearchProductViewModel>> SearchByKeyword(string keyword, int? topNumer = null)
        {
            var products = from p in _dbSet
                           where p.Status && p.Name.Contains(keyword) || p.MetaTitle.Contains(keyword)
                           orderby p.ViewCount descending
                           select new SearchProductViewModel { 
                               Id = p.Id,
                               Name = p.Name,
                               MetaTitle = p.MetaTitle,
                               Image = p.Image,
                               Price = p.Price,
                               PromotionPrice = p.PromotionPrice,
                           };
            if (topNumer != null)
                products = products.Take(topNumer.Value);
            return await products.ToListAsync();
        }
    }
}
