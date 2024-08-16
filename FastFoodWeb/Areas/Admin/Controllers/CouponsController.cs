using FastFood.Models;
using FastFood.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouponsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var coupons = _context.Coupons.ToList();
            return View(coupons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Coupon coupon = new Coupon();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Coupon coupon)
        {
            var files = Request.Form.Files;

            if(ModelState.IsValid)
            {
                byte[] photo = null;
                using(var filestream  = files[0].OpenReadStream())
                {
                    using(var memoru = new MemoryStream())
                    {
                        filestream.CopyTo(memoru);
                        photo = memoru.ToArray();   
                    }
                }


                coupon.CouponPicture = photo;
                _context.Coupons.Add(coupon);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(coupon);



        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var coupon = _context.Coupons.Where(x => x.Id == id).FirstOrDefault();

            if(coupon == null)
            {
                return NotFound();
            }

            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var coupon = _context.Coupons.Where(x=>x.Id == id).FirstOrDefault();
            if(coupon == null)
            {
                return NotFound();
            }

            return View();


        }

        [HttpPost]
        public IActionResult Edit(Coupon coupon)
        {
            var coupons = _context.Coupons.Where(x=>x.Id==coupon.Id).FirstOrDefault();
            if(ModelState.IsValid)
            {
                var files = Request.Form.Files;
                if (files.Count() > 0)
                {
                    byte[] photo = null;
                    using(var stream = files[0].OpenReadStream())
                    {
                        using(var memoryStream = new  MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            photo = memoryStream.ToArray();
                        }

                    }

                    coupons.CouponPicture = photo;

                }

                coupons.MinumumAmount=coupon.MinumumAmount;
                coupons.DisCount=coupon.DisCount;
                coupons.isActive = coupon.isActive;
                coupons.Title=coupon.Title;
                coupons.Type=coupon.Type;
                _context.Coupons.Add(coupons);
                _context.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(coupon);

        }




    }
}
