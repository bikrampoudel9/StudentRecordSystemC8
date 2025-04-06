using Microsoft.EntityFrameworkCore;
using StudentMangementSystemC8.Database.Entities;
using System.Transactions;

namespace StudentMangementSystemC8.Database
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<Student> Students { get; set; }


    }
}
