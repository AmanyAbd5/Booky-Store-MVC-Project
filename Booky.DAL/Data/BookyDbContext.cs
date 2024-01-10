using Booky.DAL.Migrations;
using Booky.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Booky.DAL.Data
{
    public class BookyDbContext: IdentityDbContext<Models.ApplicationUser>
    {
        public BookyDbContext(DbContextOptions<BookyDbContext> options):base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1,Name ="Action", DisplayOrder = 1},
                new Category { Id = 2,Name ="SciFic", DisplayOrder = 2},
                new Category { Id = 3,Name ="History",DisplayOrder = 3}
                );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1,
                   Title = "The Pragmatic Programmer",
                   Description= "A classic guide for software developers, offering practical advice on various aspects of programming and software development.",
                   ISBN= "978-0201616224",
                   Author= "Andrew Hunt, David Thomas",
                   ListPrice=200 ,Price=200 ,Price50=180, Price100=150,
                   CategoryId=12,
                   ImageURL= "/images/ThePragmaticProgrammer.jfif"
               },
               new Product
               {
                   Id = 2,
                   Title = "A Brief History of Time",
                   Description = "A bestselling exploration of the nature of the universe, explaining complex concepts in cosmology for a general audience.",
                   ISBN = "978-0553380163",
                   Author = "Stephen Hawking",
                   ListPrice = 300,
                   Price = 300,
                   Price50 = 250,
                   Price100 = 220,
                   CategoryId = 2,
                   ImageURL = "/images/ABriefHistoryofTime.jfif"
               },
               new Product
               {
                   Id = 3,
                   Title = "Mathematics: Its Content, Methods and Meaning",
                   Description = "A comprehensive overview of mathematics, providing insights into its content, methods, and broader significance.",
                   ISBN = "978-0486409160",
                   Author = "A.N. Kolmogorov, A.M. Yaglom",
                   ListPrice = 100,
                   Price = 100,
                   Price50 = 90,
                   Price100 = 50,
                   CategoryId = 1,
                   ImageURL = "/images/MathematicsItsContentMethodsandMeaning.jfif"
               },
               new Product
               {
                   Id = 4,
                   Title = "Sapiens: A Brief History of Humankind",
                   Description = "An engaging exploration of the history of the human species, from ancient times to the present, offering a fresh perspective on our evolution.",
                   ISBN = "978-0062316097",
                   Author = "Yuval Noah Harari",
                   ListPrice = 400,
                   Price = 400,
                   Price50 = 380,
                   Price100 = 360,
                   CategoryId = 3,
                   ImageURL = "/images/SapiensABriefHistoryofHumankind.jfif"
               }
               );
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Product { get; set; }

    

    }
}
