using EbookMVC.DataAccess.Data;
using EbookMVC.DataAccess.Repository.IRepository;
using EbookMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EbookMVC.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Company obj)
        {
            _context.Company.Update(obj);
        }
    }
}
