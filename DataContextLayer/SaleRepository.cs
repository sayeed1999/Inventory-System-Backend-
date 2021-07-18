using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataContextLayer
{
    public class SaleRepository : Repository<Sale>
    {
        private readonly InventoryDbContext _dbContext;
        public SaleRepository()
        {
            _dbContext = new InventoryDbContext(); // it should be done with injector!!
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
                /*var allItems = from sale in _dbContext.Sales
                               join product in _dbContext.Products on sale.ProductId equals product.Id into pg
                               join customer in _dbContext.Customers on sale.CustomerId equals customer.Id into cg
                               select new { 
                                   Id = sale.Id,
                                   Product = pg,
                                   Quantity = sale.Quantity,
                                   Customer = cg,
                                   Date = sale.Date,
                               };*/

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
