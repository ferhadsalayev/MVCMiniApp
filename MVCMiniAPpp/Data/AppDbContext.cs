using Microsoft.EntityFrameworkCore;

namespace MVCMiniAPpp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

     
        }
    }