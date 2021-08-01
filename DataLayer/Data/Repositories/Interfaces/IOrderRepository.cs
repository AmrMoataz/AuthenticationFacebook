using DataLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Data.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<TBOrders>
    {
        TBOrders GetOrderWithProducts(Guid id);
    }
}
