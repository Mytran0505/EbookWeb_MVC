using EbookWebRazor_Temp.Data;
using EbookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EbookWebRazor_Temp.Pages.Categories
{
    [BindProperties]

    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext context) { _context = context; }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _context.Categories.Find(id);
            }
        }
        public IActionResult OnPost()
        {
            if (Category.Id == null || Category.Id == 0)
            {
                return NotFound();
            }
            _context.Categories.Remove(Category);
            _context.SaveChanges();
            //sTempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
