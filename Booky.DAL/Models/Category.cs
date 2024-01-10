using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Range(1,100)]
        [Required(ErrorMessage = "Order is Required")]
        public int DisplayOrder { get; set; } 

    }
}
