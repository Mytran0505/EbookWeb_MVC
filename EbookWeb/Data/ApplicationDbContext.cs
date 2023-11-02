using EbookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EbookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        } 

        public DbSet<Category> Categories {  get; set; }
    }
}
