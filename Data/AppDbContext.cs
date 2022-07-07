using GettoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GettoAPI.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Member> Members => Set<Member>();
        
    }
}