using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepositry;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepositry = employeeRepository;
        }

        public IActionResult Index()
        {
            var employee = _employeeRepositry.GetAll();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeRepositry.Add(employee);
                return RedirectToAction(nameof(Index));

            }

            return View();
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _employeeRepositry.GetById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return View(employee);
            }

        }
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if (id == null) return BadRequest();
            var employee = _employeeRepositry.GetById(id.Value);
            if (employee == null) return NotFound();
            return View(employee);

        }
        [HttpPost]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepositry.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }

            else
            {
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            return Details(id);
        }
        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id) { return BadRequest(); }
            try
            {
                _employeeRepositry.Delete(employee);
                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);



            }
            return View(employee);

        }



    }
}
