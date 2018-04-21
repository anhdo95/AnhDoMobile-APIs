using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Returns a product list that satisfies with keyword given. 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="topNumer">The number of elements to return.</param>
        /// <returns></returns>
        Task<IEnumerable<SearchProductViewModel>> SearchByKeyword(string keyword, int? topNumer = null);
        Task<IEnumerable<ProductViewModel>> GetAll();
        /// <param name="topNumer">The number of elements to return.</param>
        Task<IEnumerable<ProductViewModel>> GetBestOutstanding(int topNumer);
        // <param name="topNumer">The number of elements to return.</param>
        Task<IEnumerable<ProductViewModel>> GetBestSelling(int topNumer);
        Task<ProductDetailViewModel> GetDetail(int id);
        /// <summary>
        /// This method will find the products are the best related to this product.
        /// </summary>
        /// <param name="id">The product's id</param>
        /// <param name="topNumber">The number of elements to return.</param>
        /// <returns>The products are the best related.</returns>
        Task<IEnumerable<RelatedProductViewModel>> GetRelatedProducts(int id, int topNumber);
    }
}
