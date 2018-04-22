using Mobile.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        IRepository<Menu> MenuRepo { get; }
        IRepository<ProductSpecification> SpecificationRepo { get; }
        ICommentRepository CommentRepo { get; }
        ICartRepository CartRepo { get; }
        Task SaveAsync();
    }
}
