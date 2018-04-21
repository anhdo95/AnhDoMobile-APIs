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
       
        /// <param name="topNumer">The number of elements to return.</param>
        /// <returns></returns>
        Task<IEnumerable<ProductViewModel>> GetBestOutstanding(int topNumer);
    }
}
