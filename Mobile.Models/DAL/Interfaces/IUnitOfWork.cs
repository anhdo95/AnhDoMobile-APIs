using Mobile.Models.Entities;
using Mobile.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        IRepository<Menu> MenuRepo { get; }
        ApiViewModel GetApi(object references, string status, string statusMessage, int length);
        Task SaveAsync();
    }
}
