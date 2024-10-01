using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Company.DAL.Models;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepositry;
        private readonly IDepartmentRepositry _departmentRepositry;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,IDepartmentRepositry departmentRepositry,
            IMapper mapper)
        {
            _employeeRepositry = employeeRepository;
            _departmentRepositry = departmentRepositry;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchValue)
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employee = _employeeRepositry.GetAll();
            }else
                employee=_employeeRepositry.GetEmployeesByName(SearchValue);
             
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employee);
            return View(MappedEmployees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Department = _employeeRepositry.GetAll();

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var MappedEmployee = _mapper.Map < EmployeeViewModel, Employee>(employeeVM);
              
                int result = _employeeRepositry.Add(MappedEmployee);
                if (result > 0)
                {
                    TempData["Message"] = "Employee Is Created";

                }

               
                return RedirectToAction(nameof(Index));

            }

            return View();
        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _employeeRepositry.GetById(id.Value);
            if (employee == null)
            
                return NotFound();
                var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);


                return View(ViewName, MappedEmployee);


            
        }
        [HttpGet]
        public IActionResult Edit(int? id)

        {
           
            return Details(id,"Edit");

        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _employeeRepositry.Update(MappedEmployee);
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
            return View(employeeVM);
        }
        public IActionResult Delete(int? id)
        {
            return Details(id);
        }
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id) { return BadRequest(); }
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee> (employeeVM);
                _employeeRepositry.Delete(MappedEmployee);
                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);



            }
            return View(employeeVM);

        }



    }
}
