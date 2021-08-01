using BussinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services.Interfaces
{
    public interface IProductService
    {
        DTOProductForm Create(DTOProductForm product);
        DTOProductForm Update(Guid id, DTOProductForm product);
        List<DTOProductForm> GetAll();
        DTOProductForm GetOne(Guid id);
        bool Delete(Guid id);
    }
}
