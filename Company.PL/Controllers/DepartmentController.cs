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
                return NotFound();
            }
            else
            {
                return View(department);
            }

        }
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if(id == null) return BadRequest();
            var department = _departmentRepositry.GetById(id.Value);
            if (department == null) return NotFound();  
            return View(department);

        }
        [HttpPost]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepositry.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
                
            }

            else { 
            }
            return View();
        }
        public IActionResult Delete(int? id) { 
            return Details(id);
        }
        [HttpPost]
        public IActionResult Delete(Department department, [FromRoute] int id) { 
            if(id != department.Id) { return BadRequest(); }
            try
            {
                _departmentRepositry.Delete(department);
                return RedirectToAction(nameof(Index));

            }
            catch(System.Exception ex) {
                ModelState.AddModelError(string.Empty,ex.Message);
            


            }return View(department);
        
        }
        


    }
}
