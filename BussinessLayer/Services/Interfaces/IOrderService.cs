using BussinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services.Interfaces
{
    public interface IOrderService 
    {
        DTOOrder Create(DTOOrderDetails order);
        List<DTOOrder> GetAll();
        DTOOrderDetails GetOne(Guid id);
        bool Delete(Guid id);
    }
}
