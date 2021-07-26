using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.EntityLayer;

namespace Inventory.ServiceLayer.CustomerService
{
    public interface ICustomerService
    {
        public Task<ServiceResponse<IEnumerable<Customer>>> GetAll(); // the 'async' modifier can only be used in methods that have a body.
        public Task<ServiceResponse<Customer>> GetById(int id);
        public Task<ServiceResponse<Customer>> Add(Customer customer);
        public Task<ServiceResponse<Customer>> Update(Customer customer);
        public Task<ServiceResponse<Customer>> Delete(int id);

    }
}
