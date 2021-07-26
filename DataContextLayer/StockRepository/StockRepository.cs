using Inventory.DataContextLayer.Repository;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataContextLayer.StockRepository
{
    public class StockRepository : Repository<Stock>
    {
        private readonly InventoryDbContext _dbContext;
        public StockRepository(InventoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ServiceResponse<IEnumerable<Stock>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Stock>>();
            try
            {
                var allItems = await _dbContext.Stocks
                    .Include(s => s.Product)
                    .ToListAsync();
                serviceResponse.Data = allItems;
                serviceResponse.Message = "Data fetched successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public override async Task<ServiceResponse<Stock>> Add(Stock item)
        {
            var serviceResponse = new ServiceResponse<Stock>();
            try
            {
                item.Id = 0;
                _dbContext.Stocks.Add(item);

                var product = await _dbContext.Products.FindAsync(item.ProductId);
                if (product == null) throw new Exception("Error doing operation.");
                product.AvailableQuantity = product.AvailableQuantity + item.Quantity;
                _dbContext.Products.Update(product);

                await _dbContext.SaveChangesAsync();
                serviceResponse.Data = item;
                serviceResponse.Message = "Data added successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

    }
}
