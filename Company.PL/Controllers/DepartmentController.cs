using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
       // private IDepartmentRepositry _departmentRepositry;
        private readonly IUnitOfWork _unitofwork;

        public DepartmentController(IUnitOfWork unitofwork)
        {
           // _departmentRepositry = departmentRepositry;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            var department = _unitofwork.departmentRepository.GetAll();
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
                _unitofwork.departmentRepository.Add(department);
                int result = _unitofwork.Complete();

                if(result > 0)
                {
                    TempData["Message"] = "Department is Created";
                }

                return RedirectToAction(nameof(Index));

            }

            return View(department);
        }
        public IActionResult Details(int? id,string ViewName = "Edit") {
            if(id == null)
          return BadRequest();
            
            var department = _unitofwork.departmentRepository.GetById(id.Value);
            if (department == null) 
                return NotFound();
            
            
            
                return View(ViewName,department);
            

        }
        [HttpGet]
        public IActionResult Edit(int? id)

        {
             
            return Details(id,"Edit");

        }
        [HttpPost]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.departmentRepository.Update(department);
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
                _unitofwork.departmentRepository.Delete(department);
                return RedirectToAction(nameof(Index));

            }
            catch(System.Exception ex) {
                ModelState.AddModelError(string.Empty,ex.Message);
            


            }return View(department);
        
        }
        


    }
}
