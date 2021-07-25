using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Inventory.DataContextLayer;
using Inventory.DataContextLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly InventoryDbContext _dbContext;
        private readonly IRepository<Category> _repository;
        public CategoryService(InventoryDbContext context, IRepository<Category> repository) // injected from startup.cs
        {
            _dbContext = context;
            _repository = repository; // how to use ninject !?
        }

        public async Task<ServiceResponse<Category>> Add(Category category)
        {
            category.Id = 0;
            return await _repository.Add(category);
        }

        public async Task<ServiceResponse<Category>> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<ServiceResponse<IEnumerable<Category>>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ServiceResponse<Category>> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ServiceResponse<Category>> Update(Category category, int id)
        {
            category.Id = id;
            return await _repository.Update(category, id);
        }
    }
}
