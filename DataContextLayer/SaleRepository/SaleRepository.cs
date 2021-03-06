using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer.Repository;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataContextLayer.SaleRepository
{
    public class SaleRepository : Repository<Sale>
    {
        private readonly InventoryDbContext _dbContext;
        public SaleRepository(InventoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ServiceResponse<IEnumerable<Sale>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Sale>>();
            try
            {
                var allItems = await _dbContext.Sales
                    .Include(s => s.Customer)
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

        public override async Task<ServiceResponse<Sale>> Add(Sale item)
        {
            var serviceResponse = new ServiceResponse<Sale>();
            try
            {
                item.Id = 0;
                _dbContext.Sales.Add(item);

                var product = await _dbContext.Products.FindAsync(item.ProductId);
                if (product == null) throw new Exception("Error doing operation.");
                if (product.AvailableQuantity < item.Quantity) throw new Exception("There are less items in stock.");
                product.AvailableQuantity = product.AvailableQuantity - item.Quantity;
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
