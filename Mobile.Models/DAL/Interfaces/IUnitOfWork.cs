﻿using Mobile.Models.Entities;
using System;
using System.Threading.Tasks;

namespace Mobile.Models.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        IRepository<Menu> MenuRepo { get; }
        Task SaveAsync();
    }
}
