using Company.BLL.Interfaces;
using Company.DAL.Contexs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositries
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepositry departmentRepository { get ; set ; }
        public UnitOfWork(AppDbContext dbContext) {
            EmployeeRepository = new  EmployeeRepositry(dbContext);
            departmentRepository = new DepartmenrRepositry(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
            
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
