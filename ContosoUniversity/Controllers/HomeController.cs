using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookStoreContext _context;

        public HomeController(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IQueryable<string> categoryQuery = from c in _context.Categories
                                               select c.Name;

            IQueryable<string> supplierQuery = from s in _context.Suppliers
                                               select s.Name;

            var books = from s in _context.Books
                        select s;

            var model = new SuppliersCategoriesBooksViewModel()
            {
                CategoriesList = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                SupplierList = new SelectList(await supplierQuery.Distinct().ToListAsync()),
                Books = await books.Take(8).ToListAsync(),
            };

            return View(model);
            //var bookStoreContext = _context.Books.Include(b => b.Category).Include(b => b.Supplier);
            //return View(await bookStoreContext.ToListAsync());
        }

        public async Task<IActionResult> Book(string searchString, string category, string supplier, int? pageNumber, string currentFilter)
        {            
            ViewData["Category"] = category;
            ViewData["Supplier"] = supplier;

            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var books = from s in _context.Books
                        select s;
            IQueryable<string> categoryQuery = from c in _context.Categories
                                               select c.Name;

            IQueryable<string> supplierQuery = from s in _context.Suppliers
                                               select s.Name;

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(category) && !String.IsNullOrEmpty(supplier))
            {
                books = books.Where(s => s.Name.Contains(searchString) && s.Category.Name.Contains(category) && s.Supplier.Name.Contains(supplier));
            }
            else if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(category))
            {
                books = books.Where(s => s.Name.Contains(searchString) && s.Category.Name.Contains(category));
            }
            else if (!String.IsNullOrEmpty(category) && !String.IsNullOrEmpty(supplier))
            {
                books = books.Where(s => s.Category.Name.Contains(category) && s.Supplier.Name.Contains(supplier));
            }
            else if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(supplier))
            {
                books = books.Where(s => s.Name.Contains(searchString) && s.Supplier.Name.Contains(supplier));
            }
            else if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Name.Contains(searchString));
            }
            else if(!String.IsNullOrEmpty(category))
            {
                books = books.Where(s => s.Category.Name.Contains(category));
            }
            else if (!String.IsNullOrEmpty(supplier))
            {
                books = books.Where(s => s.Supplier.Name.Contains(supplier));
            }

            int pageSize = 8;
            int totalPages = (int)Math.Ceiling(books.Count() / (double)pageSize);
            var model = new SuppliersCategoriesBooksViewModel()
            {
                Suppliers = await _context.Suppliers.ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                CategoriesList = new SelectList(await categoryQuery.Distinct().ToListAsync()),
                SupplierList = new SelectList(await supplierQuery.Distinct().ToListAsync()),
                Books = await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize),
                PageIndex = pageNumber ?? 1,
                TotalPages = totalPages,
            };
                        
            return View(model);
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
