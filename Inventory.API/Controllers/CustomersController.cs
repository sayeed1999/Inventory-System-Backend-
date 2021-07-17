﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Inventory.ServiceLayer.CustomerService;


namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<IEnumerable<Customer>>>> Get()
        {
            return Ok(await _customerService.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Customer>>> Get(int id)
        {
            var serviceResponse = await _customerService.GetSingleById(id);
            if (serviceResponse.Success == false) return NotFound();
            return Ok(serviceResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Customer>>> Post(Customer customer)
        {
            var serviceResponse = await _customerService.Add(customer);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return BadRequest(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Customer>>> Put(Customer customer)
        {
            var serviceResponse = await _customerService.Add(customer);
            return Ok(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<Customer>>> Delete(Customer customer)
        {
            var serviceResponse = await _customerService.Delete(customer);
            return Ok(serviceResponse);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Customer>>> Delete(int id)
        {
            var serviceResponse = await _customerService.Delete(id);
            if (serviceResponse.Success) return Ok(serviceResponse);
            return NotFound(serviceResponse);
        }
    }
}
