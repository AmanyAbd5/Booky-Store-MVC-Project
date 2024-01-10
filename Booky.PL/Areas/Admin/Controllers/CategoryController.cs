using Booky.BL.interfaces;
using Booky.BL.Repository;
using Booky.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booky.PL.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult>  Index()
        {
            var Categories =await unitOfWork.CategoryRepository.GetAll();
            if(Categories is null)
            {
                return BadRequest();
            }
            return View(Categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if(ModelState.IsValid)
            {
               await unitOfWork.CategoryRepository.add(model);
               await unitOfWork.save();
                TempData["success"] = "The Category Created Successfuly :)";
                return RedirectToAction("Index");   
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == 0 || id == null)
            {
                return BadRequest();
            }
            Category category=await unitOfWork.CategoryRepository.GetById(id);

            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async  Task<IActionResult> Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CategoryRepository.update(model);
               await unitOfWork.save();
                TempData["success"] = "The Category Updated Successfuly :)";
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id ==null)
            {
                return BadRequest();
            }
            Category category=await unitOfWork.CategoryRepository.GetById(id);
            

            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            Category category =await unitOfWork.CategoryRepository.GetById(id);
            if (category==null)
            {
                return NotFound();
            }
            unitOfWork.CategoryRepository.delete(category);
           await unitOfWork.save();
            TempData["success"] = "The Category Deleted Successfuly :)";
            return RedirectToAction("Index");
        }

    }
}
