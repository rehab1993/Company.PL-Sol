﻿using Company.BLL.Interfaces;
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
                int result = _departmentRepositry.Add(department);
                if(result > 0)
                {
                    TempData["Message"] = "Department is Created";
                }

                return RedirectToAction(nameof(Index));

            }

            return View();
        }
        public IActionResult Details(int? id,string ViewName = "Edit") {
            if(id == null)
          return BadRequest();
            
            var department = _departmentRepositry.GetById(id.Value);
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
