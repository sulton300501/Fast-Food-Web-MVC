﻿using FastFood.Models;
using FastFood.Repository;
using FastFoodWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {

        
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }




     /*   shu yer*/




        [HttpGet]
        public IActionResult Index()
        {
           
            var result = _context.Categories.ToList();
            var categModel = result.Select(x =>
            new CategoryViewModels()
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();

            return View(categModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CategoryViewModels category = new CategoryViewModels();
            return View(category);
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModels vm)
        {


            if (ModelState.IsValid)
            {
                Category model = new Category
                {
                    Title = vm.Title
                };
                _context.Categories.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);


        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _context.Categories
                 .Where(x => x.Id == id).Select(x => new CategoryViewModels()
                 {
                     Id = x.Id,
                     Title = x.Title,
                 }).FirstOrDefault();
                 


            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModels vm)
        {
            if (ModelState.IsValid)
            {
                var categoryFromDb = _context.Categories.FirstOrDefault(x=>x.Id== vm.Id);
             if(categoryFromDb != null)
                {
                    categoryFromDb.Title = vm.Title;
                    _context.Categories.Update(categoryFromDb);
                    _context.SaveChanges();
                }

            }
            return RedirectToAction("Index");
           


        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories
                 .Where(x => x.Id == id).FirstOrDefault();

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }













    }
}
