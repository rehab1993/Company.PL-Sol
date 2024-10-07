using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepositry departmentRepository { get; set; }

        Task<int> CompleteAsync();
        void Dispose();
    }
}
