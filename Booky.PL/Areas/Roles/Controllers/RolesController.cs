using AutoMapper;
using Booky.DAL.Models;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Booky.PL.Areas.Roles.Controllers
{
    [Area("Roles")]
    [Authorize]
    public class RolesController : Controller
    {

        #region ctor
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.Select(U => new RolesViewModel()
            {
                Id = U.Id,
                name = U.Name,
                NormalizedName = U.NormalizedName
            }).ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> getall()
        {
            var roles = await roleManager.Roles.Select(U => new RolesViewModel()
            {
                Id = U.Id,

                name = U.Name,
                NormalizedName = U.NormalizedName
            }).ToListAsync();

            return Json(new { data = roles });
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is IdentityRole IdentityRole)
            {
                var mapperRole = mapper.Map<IdentityRole, RolesViewModel>(IdentityRole);
                return View(mapperRole);
            }
            else
            {
                return NotFound();
            }
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RolesViewModel model, [FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(id);
                mapper.Map(model, role);
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        #endregion

        #region CreateRole
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = new ApplicationRole()
                {
                    Id  = Guid.NewGuid().ToString(),
                    Name = model.name,
                    NormalizedName = model.name,
                };
                var result = await roleManager.CreateAsync(mappedRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index)); 
                }
                else
                {
                    //  ModelState.AddModelError(string.Empty, "Faild to add Role");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        #endregion

        #region DeleteRole
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            var mapperRole = mapper.Map<IdentityRole, RolesViewModel>(role);
            return View(mapperRole);
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost([FromRoute] string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var result = await roleManager.DeleteAsync(role);

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
