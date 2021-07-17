using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.StockService
{
    public class StockService : IStockService
    {
        private InventoryDbContext _dbContext;
        public StockService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Stock>> Add(Stock stock)
        {
            var singleResponse = new ServiceResponse<Stock>();
            singleResponse.Data = stock;
            _dbContext.Stocks.Add(singleResponse.Data);
            await _dbContext.SaveChangesAsync();
            singleResponse.Message = "New object created!";
            return singleResponse;
        }

        public async Task<ServiceResponse<Stock>> Delete(Stock stock)
        {
            var singleResponse = new ServiceResponse<Stock>();
            var original_item = await _dbContext.Stocks.FindAsync(stock.Id); // i assume it will be selected from the existing database, so it will be there
            _dbContext.Stocks.Remove(original_item);
            await _dbContext.SaveChangesAsync();
            singleResponse.Data = stock;
            singleResponse.Message = "Object deleted successfully";
            return singleResponse;
        }

        public async Task<ServiceResponse<Stock>> Delete(int id)
        {
            var singleResponse = new ServiceResponse<Stock>();
            var original_item = await _dbContext.Stocks.FindAsync(id);
            if (original_item != null)
            {
                _dbContext.Stocks.Remove(original_item);
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

        public async Task<ServiceResponse<IEnumerable<Stock>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Stock>>();
            serviceResponse.Data = await _dbContext.Stocks.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Stock>> GetSingleById(int id)
        {
            var serviceResponse = new ServiceResponse<Stock>();
            serviceResponse.Data = await _dbContext.Stocks.FindAsync(id);
            if (serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No Item with this Id";
            }
            return serviceResponse;
        }
    }
}
