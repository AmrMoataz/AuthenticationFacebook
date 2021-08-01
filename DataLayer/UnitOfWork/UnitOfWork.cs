using DataLayer.Data.DBContext;
using DataLayer.Data.Repositories.Classes;
using DataLayer.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DBMainContext _context;

        public UnitOfWork(DBMainContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public IProductRepository Products { get; private set;  }

        public IOrderRepository Orders { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
