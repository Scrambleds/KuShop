using KuShop.Models;
using KuShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KuShop.Controllers
{
    public class ProductController : Controller
    {
        public readonly KuShopContext _db;
        
        public ProductController(KuShopContext db) {  _db = db; }
        public IActionResult Index()
        {
            //var pd = from p in _db.Products
            //         select p;
            var pdvm = from p in _db.Products
                       join pt in _db.ProductTypes
                       on p.PdtId equals pt.PdtId
                       into join_p_pt
                       from p_pt in join_p_pt.DefaultIfEmpty()

                       join b in _db.Brands on p.BrandId equals b.BrandId
                       into join_p_b
                       from p_b in join_p_b.DefaultIfEmpty()

                       select new PdVM
                       {
                           PdId = p.PdId,
                           PdName = p.PdName,
                           PdtName = p_pt.PdtName,
                           BrandName = p_b.BrandName,
                           PdPrice = p.PdPrice,
                           PdCost = p.PdCost,
                           PdStk = p.PdStk
                       };

            if (pdvm==null) return NotFound();
            return View(pdvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(String? stext)
        {
            if(stext==null) return RedirectToAction("Index");

            var pdvm = from p in _db.Products
                       join pt in _db.ProductTypes
                       on p.PdtId equals pt.PdtId
                       into join_p_pt
                       from p_pt in join_p_pt.DefaultIfEmpty()

                       join b in _db.Brands on p.BrandId equals b.BrandId
                       into join_p_b
                       from p_b in join_p_b.DefaultIfEmpty()

                       where p.PdName.Contains(stext) || p_b.BrandName.Contains(stext) ||
                             p_pt.PdtName.Contains(stext)

                       select new PdVM
                       {
                           PdId = p.PdId,
                           PdName = p.PdName,
                           PdtName = p_pt.PdtName,
                           BrandName = p_b.BrandName,
                           PdPrice = p.PdPrice,
                           PdCost = p.PdCost,
                           PdStk = p.PdStk
                       };

            if (pdvm == null) return NotFound();
            ViewBag.stext = stext;
            return View(pdvm);

        }

        public IActionResult Create()
        {
            ViewData["Pdt"] = new SelectList(_db.ProductTypes, "PdtId", "PdtName");
            ViewData["Brand"] = new SelectList(_db.Brands, "BrandId", "BrandName");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _db.Products.Add(obj);
                    _db.SaveChanges();
                    return RedirectToAction("Index");

                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "การบันทึกข้อมูลไม่สำเร้จ กรุณาตรวจสอบ";
                return View(obj);
            }

        }
    }

}
