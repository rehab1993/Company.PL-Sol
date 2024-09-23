using Company.BLL.Repositries;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepositry<Employee>
    {
        IQueryable<Employee> GetEmployeesByAddress(string address);
        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee employee);
        //int Update(Employee employee);
        //int Delete(Employee employee);

    }
}
