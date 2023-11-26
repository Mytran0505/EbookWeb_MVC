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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _context = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _context.Product.GetAll(includeProperties:"Category").ToList();
            
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                             Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if(productVM.Product.Id == 0)
                {
                    _context.Product.Add(productVM.Product);
                }
                else
                {
                    _context.Product.Update(productVM.Product);
                }
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
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _context.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList });
        }
        #endregion
    }
}
