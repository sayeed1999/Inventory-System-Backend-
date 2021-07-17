using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;
using Inventory.DataContextLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private InventoryDbContext _dbContext;
        public CategoryService(InventoryDbContext context) // who injects it?
        {
            _dbContext = context;
        }

        public async Task<ServiceResponse<IEnumerable<Category>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<Category>>();
            serviceResponse.Data = await _dbContext.Categories.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Category>> GetSingleById(int id)
        {
            var serviceResponse = new ServiceResponse<Category>();
            serviceResponse.Data = await _dbContext.Categories.FindAsync(id);
            if(serviceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No Item with this Id";
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Category>> AddCategory(Category category)
        {
            var singleResponse = new ServiceResponse<Category>();
            singleResponse.Data = category;
            if(await _dbContext.Categories.AnyAsync(c => c.Name.ToLower().Contains( category.Name.ToLower() )) )
            {
                singleResponse.Success = false;
                singleResponse.Message = "Category is already there!";
            }
            else
            {
                _dbContext.Categories.Add(singleResponse.Data); // it will be called when the SaveChanges() isa called, so no need to use async!
                await _dbContext.SaveChangesAsync();
                singleResponse.Message = "New object created!";
            }
            return singleResponse;
        }

        public async Task<ServiceResponse<Category>> UpdateCategory(Category category)
        {
            var singleResponse = new ServiceResponse<Category>();
            var original_category = await _dbContext.Categories.FindAsync(category.Id);

            if(original_category.Name != category.Name) original_category.Name = category.Name;
            if(original_category.Description != category.Description) original_category.Description = category.Description;
            await _dbContext.SaveChangesAsync();

            singleResponse.Data = original_category;
            singleResponse.Message = "Object successfully modified!";
            return singleResponse;
        }

        public async Task<ServiceResponse<Category>> DeleteCategory(Category category)
        {
            var singleResponse = new ServiceResponse<Category>();
            var original_category = await _dbContext.Categories.FirstAsync(c => c.Id == category.Id); // i assume it will be selected from the existing database, so it will be there
            _dbContext.Categories.Remove(original_category);
            await _dbContext.SaveChangesAsync();
            singleResponse.Data = original_category;
            singleResponse.Message = "Object deleted successfully";
            return singleResponse;
        }
        
        public async Task<ServiceResponse<Category>> DeleteCategory(int id)
        {
            var singleResponse = new ServiceResponse<Category>();
            var original_category = await _dbContext.Categories.FindAsync(id);
            if(original_category != null)
            {
                _dbContext.Categories.Remove(original_category); //it will be removed after the SaveChanges() is called.
                await _dbContext.SaveChangesAsync();
                singleResponse.Data = original_category;
                singleResponse.Message = "Object deleted successfully";
            }
            else
            {
                singleResponse.Message = "Object not found!";
                singleResponse.Success = false;
            }
            return singleResponse;
        }
    }
}
