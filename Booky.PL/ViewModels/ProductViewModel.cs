using Booky.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Booky.PL.ViewModels
{
    public class ProductViewModel
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
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public string? ImageURL { get; set; }

        public IFormFile? Image { get; set; }

    }
}
