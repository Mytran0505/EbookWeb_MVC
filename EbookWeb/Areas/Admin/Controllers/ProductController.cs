using EbookMVC.DataAccess.Data;
using EbookMVC.Models;
using Microsoft.AspNetCore.Mvc;
using EbookMVC.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using EbookMVC.Models.ViewModels;
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
            IEnumerable<SelectListItem> CategoryList = _context.Category.
                GetAll().Select(u=> new SelectListItem
            {
                    Text = u.Name,
                    Value = u.Id.ToString(),
            });
            return View(objProductList);
        }

        public IActionResult Upsert(int? id) //UpdateInsert
        {
            //IEnumerable<SelectListItem> CategoryList = _context.Category.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString(),
            //});
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM = new()
            {
                CategoryList = _context.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };
            if(id == null|| id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _context.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _context.Product.Add(productVM.Product);
                _context.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _context.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View(productVM);
            }
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
