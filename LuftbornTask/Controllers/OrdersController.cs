using BussinessLayer.DTOs;
using BussinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuftbornTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_orderService.GetAll());
        }
         
        [HttpGet("{id}")]
        public IActionResult GetOne(Guid id)
        {
            DTOOrderDetails order = _orderService.GetOne(id);
            if(order == null) return NotFound($"Order of id {id} was not found");
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Create(DTOOrderDetails order)
        {
            DTOOrder newOrder = _orderService.Create(order);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + newOrder.Id, newOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_orderService.Delete(id));
        }
    }
}
