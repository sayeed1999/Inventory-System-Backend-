using Inventory.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.ProductService
{
    public class ProductService : IProductService
    {
        private InventoryDbContext _dbContext;
        public ProductService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Product>> Add(Product product)
        {
            var singleResponse = new ServiceResponse<Product>();
            singleResponse.Data = product;
                try
                {
                    _dbContext.Products.Add(singleResponse.Data);
                    await _dbContext.SaveChangesAsync();
                    singleResponse.Message = "New object created!";
                }
                catch(Exception ex)
                {
                    singleResponse.Success = false;
                    singleResponse.Message = "Data Not Provided In Correct Format. Check whether CategoryId exists. Must provide Name, Price, CategoryId.";
                }
            return singleResponse;
        }

        public async Task<ServiceResponse<Product>> Delete(Product product)
        {
            var singleResponse = new ServiceResponse<Product>();
            var original_item = await _dbContext.Products.FindAsync(product.Id); // i assume it will be selected from the existing database, so it will be there
            _dbContext.Products.Remove(original_item);
            await _dbContext.SaveChangesAsync();
            singleResponse.Data = original_item;
            singleResponse.Message = "Object deleted successfully";
            return singleResponse;
        }

        public async Task<ServiceResponse<Product>> Delete(int id)
        {
            var singleResponse = new ServiceResponse<Product>();
            var original_item = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            if (original_item != null)
            {
                _dbContext.Products.Remove(original_item);
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

        public async Task<ServiceResponse<IEnumerable<Product>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Product>>();
            serviceResponse.Data = await _dbContext.Products.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> GetSingleById(int id)
        {
            var serviceResponse = new ServiceResponse<Product>();
            serviceResponse.Data = await _dbContext.Products.FindAsync(id);
            if (serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No Item with this Id";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Product>> Update(Product product)
        {
            var serviceResponse = new ServiceResponse<Product>();
            // i assume that the id of the product will be a valid one, because it will be selected among the existing ones from frontend.
            var original_product = await _dbContext.Products.FindAsync(product.Id);
            
            if(original_product.Name != product.Name) original_product.Name = product.Name;
            if (original_product.Price != product.Price) original_product.Price = product.Price;
            if(original_product.CategoryId != product.CategoryId) original_product.CategoryId = product.CategoryId;

            await _dbContext.SaveChangesAsync();
            serviceResponse.Data = product;
            serviceResponse.Message = "Successfully updated!";
            return serviceResponse;
        }
    }
}
