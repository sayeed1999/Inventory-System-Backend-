using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Inventory.ServiceLayer.ProductService;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Product>>>> Get()
        {
            var serviceResponse = await _productService.GetAll();
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> Get(int id)
        {
            var serviceResponse = await _productService.GetById(id);
            if (serviceResponse.Success == false) return NotFound();
            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Product>>> Put(Product product)
        {
            var serviceResponse = await _productService.Update(product);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Product>>> Post(Product product)
        {
            var serviceResponse = await _productService.Add(product);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Product>>> Delete(int id)
        {
            var serviceResponse = await _productService.Delete(id);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

    }
}
