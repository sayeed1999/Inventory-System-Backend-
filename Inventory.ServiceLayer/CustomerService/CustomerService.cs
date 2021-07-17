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
        public CustomerService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Customer>> Add(Customer customer)
        {
            var singleResponse = new ServiceResponse<Customer>();
            singleResponse.Data = customer;
                try
                {
                    _dbContext.Customers.Add(singleResponse.Data);
                    await _dbContext.SaveChangesAsync();
                    singleResponse.Message = "New object created!";
                }
                catch (Exception ex)
                {
                    singleResponse.Success = false;
                    singleResponse.Message = "Name, Address, Contact must be provided properly.";
                }
            return singleResponse;
        }

        public async Task<ServiceResponse<Customer>> Delete(Customer customer)
        {
            var singleResponse = new ServiceResponse<Customer>();
            var original_item = await _dbContext.Customers.FindAsync(customer.Id); // i assume it will be selected from the existing database, so it will be there
            _dbContext.Customers.Remove(original_item);
            await _dbContext.SaveChangesAsync();
            singleResponse.Data = customer;
            singleResponse.Message = "Object deleted successfully";
            return singleResponse;
        }

        public async Task<ServiceResponse<Customer>> Delete(int id)
        {
            var singleResponse = new ServiceResponse<Customer>();
            var original_item = await _dbContext.Customers.FindAsync(id);
            if (original_item != null)
            {
                _dbContext.Customers.Remove(original_item);
                await _dbContext.SaveChangesAsync();
                singleResponse.Message = "Object deleted successfully";
            }
            else
            {
                singleResponse.Message = "Object not found!";
                singleResponse.Success = false;
            }
            singleResponse.Data = original_item;
            return singleResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Customer>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Customer>>();
            serviceResponse.Data = await _dbContext.Customers.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Customer>> GetSingleById(int id)
        {
            var serviceResponse = new ServiceResponse<Customer>();
            serviceResponse.Data = await _dbContext.Customers.FindAsync(id);
            if (serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No Item with this Id";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Customer>> Update(Customer customer)
        {
            var serviceResponse = new ServiceResponse<Customer>();
            // i assume it exists by the id
            var original_item = await _dbContext.Customers.FindAsync(customer.Id);
            if (original_item.Name != customer.Name) original_item.Name = customer.Name;
            if (original_item.Contact != customer.Contact) original_item.Contact = customer.Contact;
            if (original_item.Address != customer.Address) original_item.Address = customer.Address;
            await _dbContext.SaveChangesAsync();
            return serviceResponse;
        }
    }
}
