using Microsoft.EntityFrameworkCore;
using stocksTracker.Models;

namespace stocksTracker.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
            : base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stock { get; set; }
    }
}
