using FastFood.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastFoodWeb.Areas.Admin.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
           var items = _context.Items.Include(x=>x.Category).Include(x=>x.SubCategory).ToList();
            return View(items);

        }


        public IActionResult Create()
        {
            return View();
        }





    }
}
