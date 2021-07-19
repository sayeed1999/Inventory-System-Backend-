using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Inventory.DataContextLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private InventoryDbContext _dbContext;
        private IRepository<Customer> _repository;
        public CustomerService(InventoryDbContext dbContext, IRepository<Customer> repository)
        {
            _dbContext = dbContext; // asp.net core injector will inject it!
            _repository = repository;
        }

        public async Task<ServiceResponse<Customer>> Add(Customer customer)
        {
            customer.Id = 0;
            return await _repository.Add(customer);
        }

        public async Task<ServiceResponse<Customer>> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<ServiceResponse<IEnumerable<Customer>>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ServiceResponse<Customer>> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ServiceResponse<Customer>> Update(Customer customer, int id)
        {
            customer.Id = id;
            return await _repository.Update(customer, id);
        }
    }
}
