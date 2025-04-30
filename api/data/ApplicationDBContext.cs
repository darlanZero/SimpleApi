using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }  
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StockModel>().ToTable("Stocks").HasKey(s => s.Id);
            builder.Entity<CommentModel>().ToTable("Comments").HasKey(c => c.Id);
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Id = "2",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole()
                {
                    Id = "3",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}