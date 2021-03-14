using Tzedakah_DataAccess.Data;
using Tzedakah_DataAccess.Repository.IRepository;
using Tzedakah_Models;
using Tzedakah_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tzedakah.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catRepo;

        public CategoryController(ICategoryRepository catRepo)
        {
            _catRepo = catRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objList = _catRepo.GetAll();
            return View(objList);
        }

        //Get Create
        public IActionResult Create()
        {
            return View();
        }



        //Post Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
               _catRepo.Add(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error while creating category!";
            return View(obj);
         
        }


        //Get Edit
        public IActionResult Edit(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _catRepo.Find(Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
               _catRepo.Update(obj);
               _catRepo.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }



        //Get Delete
        public IActionResult Delete(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var obj = _catRepo.Find(Id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound(); 
            }
            _catRepo.Remove(obj);
            _catRepo.Save();
            TempData[WC.Error] = "Category Successfully Deleted!";
            return RedirectToAction("Index");

        }



    }
}
