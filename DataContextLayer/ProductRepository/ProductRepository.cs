using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer.Repository;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataContextLayer.ProductRepository
{
    public class ProductRepository : Repository<Product>
    {
        private readonly InventoryDbContext _dbContext;
        public ProductRepository(InventoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ServiceResponse<IEnumerable<Product>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Product>>();
            try
            {
                var allItems = await _dbContext.Products
                    .Include(p => p.Category)
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
    }
}
