using Company.BLL.Interfaces;
using Company.DAL.Contexs;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositries
{
    public class DepartmenrRepositry : IDepartmentRepositry
    {
        private readonly AppDbContext _dbcontext;
        public DepartmenrRepositry(AppDbContext dbcontext) { 
            _dbcontext = dbcontext;
        }
        public IEnumerable<Department> GetAll() =>
            _dbcontext.Departments.ToList();
        
           
      
        public Department GetById(int id)
        {
            var Department = _dbcontext.Departments.Find(id);
            return Department;
        }
        public int Add(Department department)
        {
            _dbcontext.Add(department);
            return _dbcontext.SaveChanges();
        }
        public int Update(Department department)
        {
            _dbcontext.Update(department);
            return _dbcontext.SaveChanges();
            
        }

        public int Delete(Department department)
        {
            _dbcontext.Remove(department);
            return _dbcontext.SaveChanges() ;
        }

       

       

       
    }
}
