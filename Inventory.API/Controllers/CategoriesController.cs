using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.ServiceLayer.CategoryService;
using Inventory.EntityLayer;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Category>>>> Get()
        {
            var serviceResponse = await _categoryService.GetAll();
            if(serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Category>>> Get(int id)
        {
            var serviceResponse = await _categoryService.GetById(id);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);    
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Category>>> Post(Category category)
        {
            var serviceResponse = await _categoryService.Add(category);
            if(serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse); //i didn't implement any BadRequest, may be it was needed here..
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Category>>> Put(Category category)
        {
            var serviceResponse = await _categoryService.Update(category);
            if(!serviceResponse.Success) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Category>>> Delete(int id)
        {
            var serviceResponse = await _categoryService.Delete(id);
            if(serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

    }
}
