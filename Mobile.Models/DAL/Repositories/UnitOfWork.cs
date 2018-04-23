using Mobile.Models.DAL.Interfaces;
using Mobile.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MobileDbContext _context;
        private IProductRepository _productRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<Menu> _menuRepo;
        private IRepository<ProductSpecification> _specificationRepo;
        private ICommentRepository _commentRepo;
        private ICartRepository _cartRepo;
        private ICustomerRepository _customerRepo;
        private IOrderRepository _orderRepo;
        private IRepository<OrderDetail> _orderDetailRepo;

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

        public IRepository<Category> CategoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                    _categoryRepo = new Repository<Category>(_context);
                return _categoryRepo;
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

        public IRepository<ProductSpecification> SpecificationRepo
        {
            get
            {
                if (_specificationRepo == null)
                    _specificationRepo = new Repository<ProductSpecification>(_context);
                return _specificationRepo;
            }
        }

        public ICommentRepository CommentRepo
        {
            get
            {
                if (_commentRepo == null)
                    _commentRepo = new CommentRepository(_context);
                return _commentRepo;
            }
        }

        public ICartRepository CartRepo
        {
            get
            {
                if (_cartRepo == null)
                    _cartRepo = new CartRepository(_context, this);
                return _cartRepo;
            }
        }

        public ICustomerRepository CustomerRepo
        {
            get
            {
                if (_customerRepo == null)
                    _customerRepo = new CustomerRepository(_context);
                return _customerRepo;
            }
        }

        public IOrderRepository OrderRepo
        {
            get
            {
                if (_orderRepo == null)
                    _orderRepo = new OrderRepository(_context, this);
                return _orderRepo;
            }
        }

        public IRepository<OrderDetail> OrderDetailRepo
        {
            get
            {
                if (_orderDetailRepo == null)
                    _orderDetailRepo = new Repository<OrderDetail>(_context);
                return _orderDetailRepo;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
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
