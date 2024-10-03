using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            var department =await _unitofwork.departmentRepository.GetAllAsync();
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
              await  _unitofwork.departmentRepository.AddAsync(department);
                int result = await _unitofwork.CompleteAsync();

                if(result > 0)
                {
                    TempData["Message"] = "Department is Created";
                }

                return RedirectToAction(nameof(Index));

            }

            return View(department);
        }
        public async Task<IActionResult> Details(int? id,string ViewName = "Details") {
            if(id == null)
          return BadRequest();
            
            var department =await _unitofwork.departmentRepository.GetByIdAsync(id.Value);
            if (department is null) 
                return NotFound();
            
            
            
                return View(ViewName,department);
            

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)

        {
             
            return await Details(id,"Edit");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if(id != department.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitofwork.departmentRepository.Update(department);
                   await _unitofwork.CompleteAsync();
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
        public async Task<IActionResult> Delete(int? id) { 
            return await Details(id,"Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id) { 
            if(id != department.Id) { return BadRequest(); }
            try
            {
                 _unitofwork.departmentRepository.Delete(department);
                await _unitofwork.CompleteAsync();
                return RedirectToAction(nameof(Index));

            }
            catch(System.Exception ex) {
                ModelState.AddModelError(string.Empty,ex.Message);
            


            }return View(department);
        
        }
        


    }
}
