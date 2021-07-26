using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inventory.EntityLayer;
using Inventory.ServiceLayer.SalesService;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        public readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            this._salesService = salesService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Sale>>>> Get()
        {
            var serviceResponse = await _salesService.GetAll();
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Get(int id)
        {
            var serviceResponse = await _salesService.GetById(id);
            if (serviceResponse.Success == false) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Sale>>> Post(Sale sale)
        {
            var serviceResponse = await _salesService.Add(sale);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Sale>>> Put(Sale sale)
        {
            var serviceResponse = await _salesService.Update(sale);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Sale>>> Delete(int id)
        {
            var serviceResponse = await _salesService.Delete(id);
            if (!serviceResponse.Success) return NotFound(serviceResponse);
            return Ok(serviceResponse);
        }


    }
}
