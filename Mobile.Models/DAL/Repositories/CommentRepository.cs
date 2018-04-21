using System.Collections.Generic;
using System.Threading.Tasks;
using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System.Linq;
using Mobile.Models.ViewModels;

namespace Mobile.Models.DAL.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(MobileDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsByProductId(int productId)
        {
            return await Select(
                 c => new CommentViewModel
                 {
                     Id = c.Id,
                     ParentId = c.ParentId,
                     Content = c.Content,
                     Commentator = c.User.DisplayName,
                     CreatedDate = c.CreatedDate
                 },
                 filter: c => c.Status && c.ProductId == productId,
                 orderBy: list => list.OrderByDescending(c => c.CreatedDate));
        }
    }
}
