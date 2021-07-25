using Inventory.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Inventory.DataContextLayer.Repository;

namespace Inventory.ServiceLayer.SalesService
{
    public class SalesService : ISalesService
    {
        private readonly IRepository<Sale> _repository;
        public SalesService(InventoryDbContext dbContext, IRepository<Sale> repository)
        {
            _repository = repository; //overriden to SaleRepository in Startup.cs configuration!
        }

        public async Task<ServiceResponse<Sale>> Add(Sale sale)
        {
            sale.Id = 0;
            return await _repository.Add(sale);
        }

        public async Task<ServiceResponse<Sale>> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<ServiceResponse<IEnumerable<Sale>>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ServiceResponse<Sale>> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ServiceResponse<Sale>> Update(Sale sale, int id)
        {
            sale.Id = id;
            return await _repository.Update(sale, id);
        }
    }
}
