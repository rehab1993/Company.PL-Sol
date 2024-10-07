using AutoMapper;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanger;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> usermanger,IMapper mapper)
        {
            _usermanger = usermanger;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchvalue)
        {
            if (string.IsNullOrEmpty(searchvalue))
            {
                var users = await _usermanger.Users.Select(U => new UsersViewModel()
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    Email = U.Email,
                    PhoneNumber = U.PhoneNumber,
                    Roles = _usermanger.GetRolesAsync(U).Result

                }).ToListAsync();
                return View(users);
            }
            else
            { 
                var user = await _usermanger.FindByEmailAsync(searchvalue);
                var MappedUser = new UsersViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = _usermanger.GetRolesAsync(user).Result
                };



                return View(new List<UsersViewModel> { MappedUser });
            }
        }


        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }
        var user = await _usermanger.FindByIdAsync(id);
            if(user is null) return NotFound();
            var MappedUser = _mapper.Map<ApplicationUser,UsersViewModel>(user);


            return View(ViewName, MappedUser);



        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)

        {

            return await Details(id, "Edit");

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UsersViewModel userVM, [FromRoute] string id)
        {
            if (id != userVM.Id) { return BadRequest(); }
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _usermanger.FindByIdAsync(id);
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;
                    await _usermanger.UpdateAsync(user);
                   
                    
                   
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }

            
            return View(userVM);
        }


        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDelete( string id)
        {
         
            try
            {
                var User = await _usermanger.FindByIdAsync(id);
                await _usermanger.DeleteAsync(User);
                
                return RedirectToAction(nameof(Index));

            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction("Error", "Home");

            }
           

        }
    }
}
