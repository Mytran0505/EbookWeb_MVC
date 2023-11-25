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
            _context.Product.Update(obj);
        }
    }
}
