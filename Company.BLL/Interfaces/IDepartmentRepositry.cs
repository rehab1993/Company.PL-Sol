using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IDepartmentRepositry
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);

    }
}
