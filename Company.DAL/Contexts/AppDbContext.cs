using Company.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Contexs
{
    public class AppDbContext :IdentityDbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> option):base(option) 
        {
            

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=. ;Database=CompanyMVC;Trusted_Connection =True");
        //}
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
      
    }
}
