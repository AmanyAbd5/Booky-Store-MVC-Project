using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; } 

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required]
       
        public double ListPrice { get; set; }

        [Required]
        
        public double Price { get; set; }

        [Required]
        
        public double Price50 { get; set; }

        [Required]
        
        public double Price100 { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public string? ImageURL { get; set; }
    }
}
