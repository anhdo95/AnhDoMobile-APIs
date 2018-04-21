using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System;
using System.Threading.Tasks;
using Mobile.Models.ViewModels;

namespace Mobile.Models.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MobileDbContext _context;
        private IProductRepository _productRepo;
        private IRepository<Menu> _menuRepo;

        public UnitOfWork(MobileDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepo == null)
                    _productRepo = new ProductRepository(_context);
                return _productRepo;
            }
        }

        public IRepository<Menu> MenuRepo
        {
            get
            {
                if (_menuRepo == null)
                    _menuRepo = new Repository<Menu>(_context);
                return _menuRepo;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    disposed = true;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            // Prevent the finalizer from releasing unmanaged resources that have already been freed by IDisposable.Dispose
            GC.SuppressFinalize(this);
        }

        public ApiViewModel GetApi(object references, string status, string statusMessage, int length)
        {
            return new ApiViewModel
            {
                References = references,
                Status = status,
                StatusMessage = statusMessage,
                Length = length
            };
        }
    }
}
