using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Linq;
using Mobile.Models.ViewModels;
using System;

namespace Mobile.Models.DAL.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MobileDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SearchProductViewModel>> SearchByKeyword(string keyword, int? topNumer = null)
        {
            return await Select(
                p => new SearchProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    MetaTitle = p.MetaTitle,
                    Image = p.Image,
                    Price = p.Price,
                    PromotionPrice = p.PromotionPrice,
                },
                filter: p => p.Status && p.Name.Contains(keyword) || p.MetaTitle.Contains(keyword),
                orderBy: list => list.OrderByDescending(p => p.ViewCount),
                topNumber: topNumer ?? int.MaxValue);
        }

        public async Task<IEnumerable<ProductViewModel>> GetBestOutstanding(int topNumer)
        {
            return await Select(
                p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    MetaTitle = p.MetaTitle,
                    Image = p.Image,
                    Price = p.Price,
                    PromotionPrice = p.PromotionPrice,
                    DiscountAccompanying = p.DiscountAccompanying
                },
                filter: p => p.Status,
                orderBy: list => list.OrderByDescending(p => p.TopHot),
                topNumber: topNumer);
        }

        public async Task<IEnumerable<ProductViewModel>> GetBestSelling(int topNumer)
        {
            return await Select(
                p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    MetaTitle = p.MetaTitle,
                    Image = p.Image,
                    Price = p.Price,
                    PromotionPrice = p.PromotionPrice,
                    DiscountAccompanying = p.DiscountAccompanying
                },
                includeProperties: "OrderDetails",
                filter: p => p.Status,
                orderBy: list => list.OrderByDescending(p => p.OrderDetails.Sum(o => o.Order.Total)),
                topNumber: topNumer);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return await Select(
                p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    MetaTitle = p.MetaTitle,
                    Image = p.Image,
                    Price = p.Price,
                    PromotionPrice = p.PromotionPrice,
                    DiscountAccompanying = p.DiscountAccompanying
                });
        }
    }
}
