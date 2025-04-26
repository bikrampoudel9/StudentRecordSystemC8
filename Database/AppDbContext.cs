using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentMangementSystemC8.Database.Entities;
using System.Transactions;

namespace StudentMangementSystemC8.Database
{
    public class AppDbContext : IdentityDbContext<User,IdentityRole<long>,long>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole<long>>().HasData(
                
                new IdentityRole<long>
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "2c1332b7-db74-4d01-a589-c3adc104b5a4"
                },

                new IdentityRole<long>
                {
                    Id = 2,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "785ec4dc-6ad8-4c23-9e72-aea3c29bad10"
                },

                new IdentityRole<long>
                {
                    Id = 3,
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    ConcurrencyStamp = "17981503-47c1-4afb-b536-1fa776d42181"
                }
                
                );

        }


        public DbSet<Student> Students { get; set; }






    }
}
