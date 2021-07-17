using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.EntityLayer;

namespace Inventory.ServiceLayer.StockService
{
    public interface IStockService
    {
        public Task<ServiceResponse<IEnumerable<Stock>>> GetAll(); // the 'async' modifier can only be used in methods that have a body.
        public Task<ServiceResponse<Stock>> GetSingleById(int id);
        //search by name dei nai because stock has no name
        public Task<ServiceResponse<Stock>> Add(Stock stock);
        //update dei nai
        public Task<ServiceResponse<Stock>> Delete(Stock stock);
        public Task<ServiceResponse<Stock>> Delete(int id);

    }
}
