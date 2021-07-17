using Inventory.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.SalesService
{
    public class SalesService : ISalesService
    {
        private readonly InventoryDbContext _dbContext;
        public SalesService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ServiceResponse<Sale>> Add(Sale sale)
        {
            sale.Id = 0; sale.Date = DateTime.Now;
            var serviceResponse = new ServiceResponse<Sale>();
            _dbContext.Sales.Add(sale);
            await _dbContext.SaveChangesAsync();
            serviceResponse.Data = sale;
            serviceResponse.Message = "Object added successfully.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<Sale>();
            var item = await _dbContext.Sales.FindAsync(id);
            if(item != null)
            {
                _dbContext.Sales.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Item not found in the db.";
            }
            serviceResponse.Data = item;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> Delete(Sale sale)
        {
            //i assume it exists..
            var serviceResponse = new ServiceResponse<Sale>();
            serviceResponse.Data = sale;
            _dbContext.Remove(sale);
            serviceResponse.Message = "Deleted successfully.";
            await _dbContext.SaveChangesAsync();
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<Sale>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Sale>>();
            serviceResponse.Data = await _dbContext.Sales.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Sale>();
            var sale = await _dbContext.Sales.FindAsync(id);
            if(sale == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Object not found in db by this id.";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Sale>> Update(Sale sale)
        {
            var serviceResponse = new ServiceResponse<Sale>();
            // i assume it is from the existing ones..!
            var original_item = await _dbContext.Sales.FindAsync(sale.Id);
            if (original_item.Quantity != sale.Quantity) original_item.Quantity = sale.Quantity;
            if(original_item.ProductId != sale.ProductId) original_item.ProductId = sale.ProductId;
            if(original_item.CustomerId != sale.CustomerId) original_item.CustomerId = sale.CustomerId;
            await _dbContext.SaveChangesAsync();
            serviceResponse.Data = sale;
            serviceResponse.Message = "Updated successfully";
            return serviceResponse;
        }
    }
}
