using DataLayer.Data.Models;
using DataLayer.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data.Repositories.Classes
{
    public class ProductRepository : Repository<TBProducts>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
