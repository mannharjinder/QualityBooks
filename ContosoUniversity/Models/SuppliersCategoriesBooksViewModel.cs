using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Models
{
    public class SuppliersCategoriesBooksViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public SelectList CategoriesList { get; set; }
        public SelectList SupplierList { get; set; }
        public string SearchString { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
