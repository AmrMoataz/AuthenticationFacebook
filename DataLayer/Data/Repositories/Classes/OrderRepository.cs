using DataLayer.Data.DBContext;
using DataLayer.Data.Models;
using DataLayer.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Data.Repositories.Classes
{
    public class OrderRepository : Repository<TBOrders>, IOrderRepository
    {
        public OrderRepository(DBMainContext context) : base(context)
        {
        }

        public TBOrders GetOrderWithProducts(Guid id)
        {
            return (Context as DBMainContext).Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == id);
        }
    }
}
