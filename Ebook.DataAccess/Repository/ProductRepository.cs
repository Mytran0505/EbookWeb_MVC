using EbookMVC.DataAccess.Data;
using EbookMVC.DataAccess.Repository.IRepository;
using EbookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EbookMVC.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public void Update(Product obj)
        {
            var objFromb = _context.Product.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromb != null)
            {
                objFromb.Title = obj.Title;
                objFromb.Description = obj.Description;
                objFromb.Author = obj.Author;
                objFromb.ISBN = obj.ISBN;
                objFromb.ListPrice = obj.ListPrice;
                objFromb.Price = obj.Price;
                objFromb.Price100 = obj.Price100;
                objFromb.Price50 = obj.Price50;
                objFromb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null)
                {
                    objFromb.ImageUrl = obj.ImageUrl;
                }
            }
            //_context.Product.Update(obj);
        }
    }
}
