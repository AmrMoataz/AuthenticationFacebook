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
    public class ProductsController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductsController(IProductService productSrevice)
        {
            _productService = productSrevice;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(Guid id)
        {
            DTOProductForm product = _productService.GetOne(id);
            if (product == null) return NotFound($"Product of id {id} was not found");
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] DTOProductForm product)
        {
            product = _productService.Create(product);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + product.Id, product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_productService.Delete(id))
                return Ok();
            return NotFound($"Product was not found!");
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id,[FromBody] DTOProductForm product)
        {
            return Ok(_productService.Update(id, product));
        }
    }
}
