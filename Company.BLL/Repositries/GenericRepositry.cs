using Company.BLL.Interfaces;
using Company.DAL.Contexs;
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
        public int Add(T item)
        {
            _dbContext.Add<T>(item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T id)
        {
            _dbContext.Remove(id);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
          return  _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Update(T item)
        {
            _dbContext.Update<T>(item);
            return _dbContext.SaveChanges();    
        }
    }
}
