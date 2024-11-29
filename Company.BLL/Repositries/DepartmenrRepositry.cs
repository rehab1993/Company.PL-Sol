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
    public class DepartmenrRepositry :GenericRepositry<Department>,IDepartmentRepositry
    {
        

        public DepartmenrRepositry(AppDbContext dbcontext):base(dbcontext) 
        {
           
        }
        



    }
}
