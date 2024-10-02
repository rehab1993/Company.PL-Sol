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
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitofwork,
            IMapper mapper)
        {
           
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchValue)
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employee = _unitofwork.EmployeeRepository.GetAll();
            }else
                employee=_unitofwork.EmployeeRepository.GetEmployeesByName(SearchValue);
             
            var MappedEmployees = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employee);
            return View(MappedEmployees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Department = _employeeRepositry.GetAll();

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var MappedEmployee = _mapper.Map < EmployeeViewModel, Employee>(employeeVM);
              
                  _unitofwork.EmployeeRepository.Add(MappedEmployee);
               int result= _unitofwork.Complete();
                if (result > 0)
                {
                    TempData["Message"] = "Employee Is Created";

                }

               
                return RedirectToAction(nameof(Index));

            }

            return View(employeeVM);
        }
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
            {
                return BadRequest();
            }
            var employee = _unitofwork.EmployeeRepository.GetById(id.Value);
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
                    _unitofwork.EmployeeRepository.Update(MappedEmployee);
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
               _unitofwork.EmployeeRepository.Delete(MappedEmployee);
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
