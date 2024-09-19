using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Company.DAL.Models;
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
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentRepositry.Add(department);
                return RedirectToAction(nameof(Index));

            }

            return View();
        }
        public IActionResult Details(int? id) {
            if(id == null)
            {
                return BadRequest();
            }
            var department = _departmentRepositry.GetById(id.Value);
            if (department == null) {
                return BadRequest();
            }
            else
            {
                return View(department);
            }

        }


    }
}
