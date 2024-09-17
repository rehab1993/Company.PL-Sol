using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentRepositry _departmentRepositry;
        public DepartmentController(IDepartmentRepositry departmentRepositry)
        {
            _departmentRepositry = departmentRepositry;
        }

        public IActionResult Index()
        {
            var department = _departmentRepositry.GetAll();
            return View(department);
        }
    }
}
