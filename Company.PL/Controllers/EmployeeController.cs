using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositries;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitofwork,
            IMapper mapper)
        {
           
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(SearchValue))
            {
                employee =await _unitofwork.EmployeeRepository.GetAllAsync();
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
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
              string FileName=  DocumentSettings.UploadFile(employeeVM.Image , "Images");
                employeeVM.ImageName = FileName;
                var MappedEmployee = _mapper.Map < EmployeeViewModel, Employee>(employeeVM);
              
                 await _unitofwork.EmployeeRepository.AddAsync(MappedEmployee);
               int result=await _unitofwork.CompleteAsync();
                if (result > 0)
                {
                    TempData["Message"] = "Employee Is Created";

                }

               
                return RedirectToAction(nameof(Index));

            }

            return View(employeeVM);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
            var employee =await _unitofwork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
            
                return NotFound();
                var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);


                return View(ViewName, MappedEmployee);


            
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)

        {
           
            return await Details(id,"Edit");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id !=employeeVM.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null) {
                        employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image,"Images");
                    }
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitofwork.EmployeeRepository.Update(MappedEmployee);
                   await _unitofwork.CompleteAsync();
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


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id) { return BadRequest(); }
            try
            {
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee> (employeeVM);
               _unitofwork.EmployeeRepository.Delete(MappedEmployee);
                var result =await _unitofwork.CompleteAsync();
                if (result>0 && employeeVM.ImageName is not null) {
                    DocumentSettings.DeletFile(employeeVM.ImageName, "Images");
                        }
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
