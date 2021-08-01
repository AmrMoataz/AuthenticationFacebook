using BussinessLayer.DTOs;
using BussinessLayer.Services.Interfaces;
using DataLayer.Data.Models;
using DataLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BussinessLayer.Services.Classes
{
    public class OrderService : IOrderService
    {
        #region Private Fields
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region IOrderService Implementation
        public DTOOrder Create(DTOOrderDetails order)
        {
            TBOrders newOrder = new TBOrders();
            newOrder.Id = Guid.NewGuid();
            newOrder.Date = DateTime.Now;
            order.products.ForEach(p =>
            {
                newOrder.TotalPrice += p.Price;
                newOrder.Products.Add(_unitOfWork.Products.Get(p.Id));
            });
            _unitOfWork.Orders.Add(newOrder);
            _unitOfWork.Complete();
            DTOOrder orderObj = Populate(newOrder);

            return orderObj;
        }

        public bool Delete(Guid id)
        {
            try
            {
                TBOrders order = _unitOfWork.Orders.Get(id);
                _unitOfWork.Orders.Add(order);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<DTOOrder> GetAll()
        {
            List<TBOrders> orders = _unitOfWork.Orders.GetAll().ToList();
            List<DTOOrder> DtoOrders = new List<DTOOrder>();
            foreach (var order in orders)
            {
                DtoOrders.Add(Populate(order));
            }

            return DtoOrders;
        }

        public DTOOrderDetails GetOne(Guid id)
        {
            TBOrders order = _unitOfWork.Orders.GetOrderWithProducts(id);
            if (order == null) return null;
            return PopulateToDetails(order);
        }
        #endregion

        #region Private Func's
        /// <summary>
        /// used to reduce code duplication populate Order Models to DTOOrder Object
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>DTOOrder</returns>     
        private DTOOrder Populate(TBOrders newOrder)
        {
            DTOOrder orderObj = new DTOOrder();
            orderObj.Id = newOrder.Id;
            orderObj.Date = newOrder.Date;
            orderObj.TotalPrice = newOrder.TotalPrice;
            return orderObj;
        }

        /// <summary>
        /// used to reduce code duplication populate Order Models to DTOOrder Object
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>DTOOrder</returns>     
        private DTOOrderDetails PopulateToDetails(TBOrders newOrder)
        {
            DTOOrderDetails orderObj = new DTOOrderDetails();
            orderObj.id = newOrder.Id;
            orderObj.date = newOrder.Date;
            orderObj.totalPrice = newOrder.TotalPrice;
            newOrder.Products.ForEach(p =>
            {
                DTOProductForm product = new DTOProductForm();
                product.Id = p.Id;
                product.Name = p.Name;
                product.Price = p.Price;
                product.Qty = p.Qty;
                orderObj.products.Add(product);
            });
            return orderObj;
        }
        #endregion
    }
}
