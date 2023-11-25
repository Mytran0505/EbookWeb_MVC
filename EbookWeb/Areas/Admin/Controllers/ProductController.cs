using EbookMVC.DataAccess.Data;
using EbookMVC.Models;
using Microsoft.AspNetCore.Mvc;
using EbookMVC.DataAccess.Repository.IRepository;
namespace EbookMVCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _context;
        public ProductController(IUnitOfWork db)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _context.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _context.Product.Add(obj);
                _context.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _context.Product.Get(u => u.Id == id);
            //Product? ProductFromDb1 = _context.Categories.FirstOrDefault(u => u.Id == id);
            //Product? ProductFromDb2 = _context.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _context.Product.Update(obj);
                _context.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _context.Product.Get(u => u.Id == id);
            /*Product? ProductFromDb1 = _context.Categories.FirstOrDefault(u=>u.Id==id);
            Product? ProductFromDb2 = _context.Categories.Where(u => u.Id == id).FirstOrDefault();*/
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _context.Product.Get(u => u.Id == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            _context.Product.Remove(obj);
            _context.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
