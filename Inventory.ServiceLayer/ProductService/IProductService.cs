using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.EntityLayer;

namespace Inventory.ServiceLayer.ProductService
{
    public interface IProductService
    {
        public Task<ServiceResponse<IEnumerable<Product>>> GetAll(); // the 'async' modifier can only be used in methods that have a body.
        public Task<ServiceResponse<Product>> GetById(int id);
        public Task<ServiceResponse<Product>> Add(Product product);
        public Task<ServiceResponse<Product>> Update(Product product);
        public Task<ServiceResponse<Product>> Delete(int id);

    }
}
