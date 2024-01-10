using AutoMapper;
using Booky.DAL.Models;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Booky.PL.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class UsersController : Controller
    {


        #region Ctor
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
          this.userManager = userManager;
          this.roleManager = roleManager;
            this.mapper = mapper;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            //var users = await userManager.Users.Select(U => new UsersViewModel()
            //{
            //    Id = U.Id,
            //    Fname = U.Fname,
            //    Lname = U.Lname,
            //    Email = U.Email,
            //    Roles = userManager.GetRolesAsync(U).Result
            //}).ToListAsync();
            //return View(users);
            var users = await userManager.Users.ToListAsync();

            var usersViewModel = users.Select(U => new UsersViewModel
            {
                Id = U.Id,
                Fname = U.Fname,
                Lname = U.Lname,
                Email = U.Email,
                Roles = userManager.GetRolesAsync(U).Result
            }).ToList();

            return View(usersViewModel);

        }

        public async Task<IActionResult> getall()
        {
            var users = await userManager.Users.Select(U => new UsersViewModel()
            {
                Id = U.Id,
                Fname = U.Fname,
                Lname = U.Lname,
                Email = U.Email,
                Roles = userManager.GetRolesAsync(U).Result
            }).ToListAsync();

            return Json(new { data = users });
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var mapperUser = mapper.Map<ApplicationUser, UsersViewModel>(user);
            return View(mapperUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersViewModel model, [FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(id);
                mapper.Map(model, user);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                // Log or debug output: error.ErrorMessage or error.Exception
            }
            return View(model);
        }
        #endregion

        #region CreateUser
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allRolos = await roleManager.Roles.ToListAsync();
            var roleSlectListItem = allRolos.Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name
            });
            ViewBag.Rolos = roleSlectListItem;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedUser = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.Email.Split('@')[0],
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Email = model.Email,
                };
                var result = await userManager.CreateAsync(mappedUser, model.Password);
                if (result.Succeeded)
                {
                    if (model.Roles != null && model.Roles.Any())
                    {
                        var addedRole = await userManager.AddToRolesAsync(mappedUser, model.Roles);
                        if (addedRole.Succeeded)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Faild to add Role");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Faild to add User");
                }
            }
            return View(model);
        }
        #endregion

        #region DeleteUser
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var mapperUser = mapper.Map<ApplicationUser, UsersViewModel>(user);
            return View(mapperUser);
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost([FromRoute] string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        } 
        #endregion


    }
}
