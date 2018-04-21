using Mobile.Models.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MobileDbContext _context;
        private IProductRepository _productRepo;

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
    }
}
