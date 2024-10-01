using Company.BLL.Interfaces;
using Company.DAL.Contexs;
using Company.DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositries
{
    public class EmployeeRepositry : GenericRepositry<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepositry(AppDbContext context) :base(context) {
            _context = context;
        }
        
        
       

       
        
        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _context.Employees.Where(e => e.Address == address);
        }

        public IQueryable<Employee> GetEmployeesByName(string SearchName)
        {
         return  _context.Employees.Where(E=>E.Name.ToLower().Contains(SearchName.ToLower()));
        }
    }
}
