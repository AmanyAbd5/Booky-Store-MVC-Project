using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booky.DAL.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string Id { get; set; }

        public string name { get; set; }

        public string NormalizedName { get; set; }

        


    }
}
