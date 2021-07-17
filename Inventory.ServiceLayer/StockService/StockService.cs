using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.DataContextLayer;
using Inventory.EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace Inventory.ServiceLayer.StockService
{
    public class StockService : IStockService
    {
        private InventoryDbContext _dbContext;
        private IRepository<Stock> _repository;
        public StockService(InventoryDbContext dbContext) // injected by Asp.Net Core injector
        {
            _dbContext = dbContext;
            _repository = new Repository<Stock>();
        }

        public async Task<ServiceResponse<Stock>> Add(Stock stock)
        {
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

        public async Task<ServiceResponse<Stock>> Update(Stock stock, int id)
        {
            stock.Id = id;
            return await _repository.Update(stock, id);
        }

    }
}
