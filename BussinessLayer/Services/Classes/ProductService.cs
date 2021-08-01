using BussinessLayer.DTOs;
using BussinessLayer.Services.Interfaces;
using DataLayer.Data.Models;
using DataLayer.Data.Repositories.Interfaces;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLayer.Services.Classes
{
    public class ProductService : IProductService
    {
        #region Private Fields
        protected readonly IUnitOfWork _unitOfWork; 
        #endregion

        #region Constructors
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        } 
        #endregion

        #region IProductService Implementation
        public DTOProductForm Create(DTOProductForm product)
        {
            TBProducts newProduct = new TBProducts();
            newProduct.Id = Guid.NewGuid();
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.Qty = product.Qty;
            _unitOfWork.Products.Add(newProduct);
            _unitOfWork.Complete();
            product.Id = newProduct.Id;
            return product;
        }

        public bool Delete(Guid id)
        {
            try
            {
                TBProducts product = _unitOfWork.Products.Get(id);
                _unitOfWork.Products.Remove(product);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public List<DTOProductForm> GetAll()
        {
            List<TBProducts> products = _unitOfWork.Products.GetAll().OrderBy(p => p.Name).ToList();
            List<DTOProductForm> DTOProducts = new List<DTOProductForm>();
            foreach (var prod in products)
            {
                DTOProducts.Add(Populate(prod));
            }
            return DTOProducts;
        }

        public DTOProductForm GetOne(Guid id)
        {
            TBProducts product = _unitOfWork.Products.Get(id);
            if (product == null) return null;
            return Populate(product);
        }

        public DTOProductForm Update(Guid id, DTOProductForm product)
        {
            TBProducts newProduct = _unitOfWork.Products.Get(id);
            newProduct.Name = product.Name;
            newProduct.Price = product.Price;
            newProduct.Qty = product.Qty; 
            _unitOfWork.Complete();
            product.Id = id;
            return product;
        }

        #endregion

        #region Private Func's
        /// <summary>
        /// used to reduce code duplication populate Product Models to DTOProductForm Object
        /// </summary>
        /// <param name="prod"></param>
        /// <returns>DTOProductForm</returns>
        private DTOProductForm Populate(TBProducts prod)
        {
            DTOProductForm dtoProduct = new DTOProductForm();
            dtoProduct.Id = prod.Id;
            dtoProduct.Name = prod.Name;
            dtoProduct.Price = prod.Price;
            dtoProduct.Qty = prod.Qty;
            return dtoProduct;
        } 
        #endregion
    }
}
