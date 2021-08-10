
using FakeNewsFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeNewsFilter.Data.EF
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
    }
}
