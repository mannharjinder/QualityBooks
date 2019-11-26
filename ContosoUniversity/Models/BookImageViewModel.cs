using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Http;

namespace ContosoUniversity.Models
{
    public class BookImageViewModel
    {
        public Book Book { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
