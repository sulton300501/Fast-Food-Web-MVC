using FastFood.Models;
using FastFood.Repository;
using FastFoodWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FastFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ItemsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
           var items = _context.Items.Include(x=>x.Category).Include(x=>x.SubCategory)
                .Select(model=>new ItemViewModel()
                {
                    Id=model.Id,
                    Title=model.Title,
                    Description=model.Description,
                    Price=model.Price,
                    CategoryId=model.CategoryId,
                    SubCategoryId=model.SubCategoryId,  
                })
                .ToList();
            return View(items);

        }

        [HttpGet]
        public  IActionResult Create()
        {
            ItemViewModel vm = new ItemViewModel();
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");
            return View();
        }

        [HttpGet]
        public IActionResult GetSubCategory(int categoryId)
        {
            var subCategory = _context.SubCategories.Where(x=>x.CategoryId == categoryId).FirstOrDefault();
            return Json(subCategory);
        
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel vm)
        {
            Item model = new Item();

            if(ModelState.IsValid)
            {
                if(vm.ImageUrl!=null && vm.ImageUrl.Length>0)
                {
                    var uploadDir = @"Images/Items";
                    var fileName = Guid.NewGuid().ToString() + vm.ImageUrl.FileName;
                    var path = Path.Combine(_environment.WebRootPath, uploadDir, fileName);
                    await vm.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    model.Image = '/' + uploadDir + "/" + fileName;

                }

            model.Price = vm.Price;
            model.Description = vm.Description;
            model.Title = vm.Title;
            model.SubCategoryId = vm.SubCategoryId;
            model.CategoryId = vm.CategoryId;
                _context.Items.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }


            return View(vm);

        }





        public IActionResult Edit(int id)
        {
            var item = _context.Items.Where(x=>x.Id==id).FirstOrDefault();
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title", item.CategoryId);
            ViewBag.SubCategory = new SelectList(_context.SubCategories, "Id", "Title", item.SubCategoryId);
         
            return View(item);

        }



       


    }
}
