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
        private readonly InventoryDbContext _dbContext;
        private readonly IRepository<Product> _repository;
        public ProductService(InventoryDbContext dbContext) // this dbContext is injected from Startup.cs!
        {
            _dbContext = dbContext;
            _repository = new Repository<Product>();
        }

        public async Task<ServiceResponse<Product>> Add(Product product)
        {
            return await _repository.Add(product);
        }

        public async Task<ServiceResponse<Product>> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<ServiceResponse<IEnumerable<Product>>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ServiceResponse<Product>> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ServiceResponse<Product>> Update(Product product, int id)
        {
            product.Id = id;
            return await _repository.Update(product, id);
        }
    }
}
