using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Inventory.DataContextLayer.Repository;
using Inventory.EntityLayer;

namespace Inventory.ServiceLayer.StockService
{
    public class StockService : IStockService
    {
        private IRepository<Stock> _repository;
        public StockService(InventoryDbContext dbContext, IRepository<Stock> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<Stock>> Add(Stock stock)
        {
            //stock.Id = 0;
            return await _repository.Add(stock);
        }

        public async Task<ServiceResponse<Stock>> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<ServiceResponse<IEnumerable<Stock>>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ServiceResponse<Stock>> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<ServiceResponse<Stock>> Update(Stock stock)
        {
            //stock.Id = id;
            return await _repository.Update(stock);
        }

    }
}
