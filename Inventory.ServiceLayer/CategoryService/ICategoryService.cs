using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inventory.EntityLayer;

namespace Inventory.ServiceLayer.CategoryService
{
    public interface ICategoryService
    {
        public Task<ServiceResponse<IEnumerable<Category>>> GetAll(); // the 'async' modifier can only be used in methods that have a body.
        public Task<ServiceResponse<Category>> GetSingleById(int id);
        public Task<ServiceResponse<Category>> AddCategory(Category category);
        public Task<ServiceResponse<Category>> UpdateCategory(Category category);
        public Task<ServiceResponse<Category>> DeleteCategory(Category category);
        public Task<ServiceResponse<Category>> DeleteCategory(int id);
    }
}
