using EbookMVC.DataAccess.Data;
using EbookMVC.Models;
using Microsoft.AspNetCore.Mvc;
using EbookMVC.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using EbookMVC.Models.ViewModels;
using EbookMVC.Utility;
using Microsoft.AspNetCore.Authorization;
namespace EbookMVCWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _context;
        public CompanyController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _context = db;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _context.Company.GetAll().ToList();
            
            return View(objCompanyList);
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
            //CompanyVM CompanyVM = new()
            //{
            //    CategoryList = _context.Category
            //    .GetAll().Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString(),
            //    }),
            //    Company = new Company()
            //};
            if(id == null|| id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _context.Company.Get(u=>u.Id==id);
                return View(companyObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            
            if (ModelState.IsValid)
            {
                if (companyObj.Id==0)
                {
                    
                    _context.Company.Add(companyObj);
                }
                else
                {
                    _context.Company.Update(companyObj);
                }
                _context.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }
        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? CompanyFromDb = _context.Company.Get(u => u.Id == id);
        //    /*Company? CompanyFromDb1 = _context.Categories.FirstOrDefault(u=>u.Id==id);
        //    Company? CompanyFromDb2 = _context.Categories.Where(u => u.Id == id).FirstOrDefault();*/
        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Company? obj = _context.Company.Get(u => u.Id == id);
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    _context.Company.Remove(obj);
        //    _context.Save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction("Index");
        //}
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _context.Company.GetAll().ToList();
            return Json(new {data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDelete = _context.Company.Get(u => u.Id == id);
            if (CompanyToBeDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting"});
            }

            _context.Company.Remove(CompanyToBeDelete);
            _context.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
}
