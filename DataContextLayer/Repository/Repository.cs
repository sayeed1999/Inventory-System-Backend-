using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Inventory.EntityLayer;

namespace Inventory.DataContextLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly InventoryDbContext _dbContext;
        public Repository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<ServiceResponse<T>> Add(T item)
        {
            var serviceResponse = new ServiceResponse<T>();
            try
            {
                item.GetType().GetProperty("Id")?.SetValue(item, 0); //setting the PK of the entity as 0
                _dbContext.Set<T>().Add(item);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Data = item;
                serviceResponse.Message = "Data added successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<T>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<T>();
            var itemToBeDeleted = await _dbContext.Set<T>().FindAsync(id); // can be null
            serviceResponse.Data = itemToBeDeleted;

            if(itemToBeDeleted == null)
            {
                serviceResponse.Message = "Data not found on the database.";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            try
            {
                _dbContext.Set<T>().Remove(itemToBeDeleted);
                await _dbContext.SaveChangesAsync();
                serviceResponse.Message = "Data deleted successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
        
        public virtual async Task<ServiceResponse<IEnumerable<T>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<IEnumerable<T>>();
            try
            {
                var allItems = await _dbContext.Set<T>().ToListAsync();
                serviceResponse.Data = allItems;
                serviceResponse.Message = "Data fetched successfully.";
            }
            catch(Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<T>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<T>();
            try
            {
                serviceResponse.Data = await _dbContext.Set<T>().FindAsync(id);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.Message = "No item found in the database table with this id.";
                    serviceResponse.Success = false;
                }
                else serviceResponse.Message = "Data fetched successfully.";
            }
            catch (Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public virtual async Task<ServiceResponse<T>> Update(T item)
        {
            var serviceResponse = new ServiceResponse<T>();

            int id = (int)item.GetType().GetProperty("Id")?.GetValue(item);

            try
            {
                serviceResponse.Data = await _dbContext.Set<T>().FindAsync(id);
                if(serviceResponse.Data == null)
                {
                    serviceResponse.Message = "No item with this id in the database table.";
                    serviceResponse.Success = false;
                }
                else
                {
                    try
                    {
                        _dbContext.Entry<T>(serviceResponse.Data).CurrentValues.SetValues(item);
                        await _dbContext.SaveChangesAsync();
                        serviceResponse.Message = "Data updated successfully.";
                    }
                    catch (Exception ex)
                    {
                        serviceResponse.Message = ex.Message;
                        serviceResponse.Success = false;
                    }
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }
    }
}
