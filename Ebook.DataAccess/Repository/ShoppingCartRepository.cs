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
    public class ShoppingcartRepository : Repository<ShoppingCart>, IShoppingCartRepository 
    {
        private ApplicationDbContext _context;
        public ShoppingcartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public void Update(ShoppingCart obj)
        {
            //var objFromb = _context.ShoppingCart.FirstOrDefault(u => u.Id == obj.Id);
            //if (objFromb != null)
            //{
            //    objFromb.ProductId = obj.ProductId;
            //    objFromb.Count = obj.Count;
            //    objFromb.ApplicationUserId = obj.ApplicationUserId;
            //}
            _context.ShoppingCart.Update(obj);
        }
    }
}
