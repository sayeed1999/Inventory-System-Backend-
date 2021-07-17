using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Inventory.EntityLayer;


namespace Inventory.ServiceLayer.SalesService
{
    public interface ISalesService
    {
        public Task<ServiceResponse<IEnumerable<Sale>>> GetAll();
        public Task<ServiceResponse<Sale>> GetById(int id);
        public Task<ServiceResponse<Sale>> Add(Sale sale);
        public Task<ServiceResponse<Sale>> Update(Sale sale, int id);
        public Task<ServiceResponse<Sale>> Delete(int id);
    }
}
