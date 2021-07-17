﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Inventory.ServiceLayer.StockService;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private IStockService _stockService;
        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Stock>>>> Get()
        {
            return Ok(await _stockService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Stock>>> Get(int id)
        {
            var serviceResponse = await _stockService.GetSingleById(id);
            if (serviceResponse.Success == false) return NotFound();
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Stock>>> Post(Stock stock)
        {
            var serviceResponse = await _stockService.Add(stock);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<Stock>>> Delete(Stock stock)
        {
            var serviceResponse = await _stockService.Delete(stock);
            return Ok(serviceResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Stock>>> Delete(int id)
        {
            var serviceResponse = await _stockService.Delete(id);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }

    }
}
