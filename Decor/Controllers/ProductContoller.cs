using Decor_DataAccess.Data;
using Decor_DataAccess.Repository.IRepository;
using Decor_Models;
using Decor_Models.ViewModels;
using Decor_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Decor.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _prodRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository prodRepo, IWebHostEnvironment webHostEnvironment)
        {
            _prodRepo = prodRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            //Eager Loading
            IEnumerable<Product> objList = _prodRepo.GetAll(includeProperties:"Category,ApplicationType");

           

            //foreach (var obj in objList)
            //{
            //    obj.Category = _db.Category.FirstOrDefault(u => u.Id == obj.CategoryId);
            //    obj.ApplicationType = _db.ApplicationType.FirstOrDefault(u => u.Id == obj.ApplicationTypeId);
            //}
            return View(objList);
        }

        //Get Upsert
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            //{
            //    Text = i.Name,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;
            //Product product = new Product();

            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _prodRepo.GetAllDropdownList(WC.CategoryName),
                ApplicationTypeSelectList = _prodRepo.GetAllDropdownList(WC.ApplicationTypeName)

            };
            if (id == null)
            {
                //This is for create
                return View(productVM);
            }
            else
            {
                productVM.Product = _prodRepo.Find(id.GetValueOrDefault());
                if (productVM.Product == null)
                {
                    return NotFound();
                }



            }

         
            return View(productVM);
        }



        //Post Upsert
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.Id == 0)
                {
                    //Create
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;

                    _prodRepo.Add(productVM.Product);
                 

                }
                else
                {
                    //Update
                    var objFromDb = _prodRepo.FirstOrDefault(u => u.Id == productVM.Product.Id,isTracking:false);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _prodRepo.Update(productVM.Product);

                }

                _prodRepo.Save();
                return RedirectToAction("Index");
            }

            productVM.CategorySelectList = _prodRepo.GetAllDropdownList(WC.CategoryName);
            productVM.ApplicationTypeSelectList = _prodRepo.GetAllDropdownList(WC.ApplicationTypeName);
            return View(productVM);
         
        }



        //Get Delete
        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }

            Product product = _prodRepo.FirstOrDefault(u => u.Id == id,  includeProperties: "Category,ApplicationType");
            if (product == null)
            {
                return NotFound();
            }


            //ProductVM productVM = new ProductVM()
            //{
            //    Product = new Product(),
            //    CategorySelectList = _db.Category.Select(i => new SelectListItem
            //    {
            //        Text = i.Name,
            //        Value = i.Id.ToString()
            //    })

            //};
            //if (id == null)
            //{

            //    return NotFound();
            //}
            //else
            //{
            //    productVM.Product = _db.Product.Find(id);
            //    if (productVM.Product == null)
            //    {
            //        return NotFound();
            //    }

            //}
            //return View(productVM);
            return View(product);
        }


        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Product product)
        {
            var obj = _prodRepo.Find(product.Id);
            if (obj == null)
            {
                return NotFound(); 
            }


            string webRootPath = _webHostEnvironment.WebRootPath;

           
             string upload = webRootPath + WC.ImagePath;
          
           var oldFile = Path.Combine(upload, obj.Image);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
     



            _prodRepo.Remove(obj);
            _prodRepo.Save();
            return RedirectToAction("Index");

        }



    }
}
