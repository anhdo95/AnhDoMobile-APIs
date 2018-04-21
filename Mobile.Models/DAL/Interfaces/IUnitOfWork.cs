using System;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        Task SaveAsync();
    }
}
