using Company.BLL.Interfaces;
using Company.DAL.Contexs;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositries
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        private readonly AppDbContext _dbContext;

        public GenericRepositry(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T item)
        {
           await _dbContext.AddAsync<T>(item);
           
        }

        public void Delete(T id)
        {
            _dbContext.Remove(id);
           
        }

        public async Task <IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Employee)){

                return  (IEnumerable<T>) await _dbContext.Employees.Include(e=>e.Department).ToListAsync();
            }
          return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return  await _dbContext.Set<T>().FindAsync(id);
        }

        public void Update(T item)
        {
            _dbContext.Update<T>(item);
               
        }
    }
}
