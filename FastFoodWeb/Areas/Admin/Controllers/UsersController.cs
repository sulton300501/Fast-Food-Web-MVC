using FastFood.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FastFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


          
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                {
                    // claim topilmadi, tegishli harakatni bajaring, masalan, foydalanuvchini chiqarib yuboring yoki xato sahifasini ko'rsating
                    return RedirectToAction("Login", "Account"); // Misol uchun, foydalanuvchini qayta yo'naltirish
                }

                var userId = claim.Value;

                var users = _context.ApplicationUsers.Where(x => x.Id != userId).ToList();

                return View(users);
            



        }





    }
}
