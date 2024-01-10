using AutoMapper;
using Booky.BL.interfaces;
using Booky.BL.Repository;
using Booky.DAL.Models;
using Booky.PL.Helper;
using Booky.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booky.PL.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var Products =await unitOfWork.ProductRepository.GetAll();
            var mappedProduct = mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(Products);
            if (mappedProduct is null)
            {
                return BadRequest();
            }
           
            return View(mappedProduct);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<SelectListItem> catSelectListItems =(await unitOfWork.CategoryRepository.GetAll())
           .Select(x => new SelectListItem
           {
               Text = x.Name,
               Value = x.Id.ToString()
           });
            ViewBag.catSelectListItems = catSelectListItems;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Create(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                if (productVM.Image != null)
                {
                    productVM.ImageURL =await filesManagement.UploadFile(productVM.Image, "images");
                   
                }
                else
                {
                    productVM.ImageURL = "";
                }
                var mappedProduct = mapper.Map<ProductViewModel, Product>(productVM);
               await unitOfWork.ProductRepository.add(mappedProduct);
               await unitOfWork.save();
                TempData["success"] = "The Product Created Successfuly :)";
                return RedirectToAction("Index");

            }
            return View(productVM);
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }
            IEnumerable<SelectListItem> catSelectListItems =(await unitOfWork.CategoryRepository.GetAll())
          .Select(x => new SelectListItem
          {
              Text = x.Name,
              Value = x.Id.ToString()
          });
            ViewBag.catSelectListItems = catSelectListItems;

            Product product =await unitOfWork.ProductRepository.GetById(id);
            var mapperProduct = mapper.Map<Product, ProductViewModel>(product);
            if (mapperProduct == null)
            {
                return NotFound();
            }
            return View(mapperProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel productMV)
        {
            if (ModelState.IsValid)
            {
                if(productMV.Image != null)
                {
                    if(!string.IsNullOrEmpty(productMV.ImageURL))
                    {
                        filesManagement.DeleteFile(productMV.ImageURL, "images");
                    }
                    productMV.ImageURL =await filesManagement.UploadFile(productMV.Image, "images");
                }
                var productMapped = mapper.Map< ProductViewModel,Product>(productMV);
                unitOfWork.ProductRepository.update(productMapped);
              await  unitOfWork.save();
                TempData["success"] = "The Product Updated Successfuly :)";
                return RedirectToAction("Index");
            }
            return View(productMV);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return BadRequest();
            }

            Product product22 =await unitOfWork.ProductRepository.GetById(id);

            if (product22 == null)
            {
                return NotFound();
            }
            // Now, you can access the category name
            string categoryName = product22.Category?.Name;

            ViewBag.CategoryName = categoryName;

            Product product =await unitOfWork.ProductRepository.GetById(id);
            var mapperProduct = mapper.Map<Product, ProductViewModel>(product);

            if (mapperProduct == null)
            {
                return NotFound();
            }

            return View(mapperProduct);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var product =await unitOfWork.ProductRepository.GetById(id);
            if (product is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(product.ImageURL))
                {
                    filesManagement.DeleteFile(product.ImageURL, "images");
                }
                
            unitOfWork.ProductRepository.delete(product);
           await unitOfWork.save();
            TempData["success"] = "The Product Deleted Successfuly :)";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var products=await unitOfWork.ProductRepository.GetAll();
            return Json(new {data=products});
        }

    }
}
