using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public static class DbInitializerBookStore
    {
        public static void Initialize(BookStoreContext context)
        {
            context.Database.EnsureCreated();
            if (context.Books.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category{Code="001", Name="Arts & Music"},
                new Category{Code="002", Name="Business"},
                new Category{Code="003", Name="Sports"},
                new Category{Code="004", Name="Maori Culture"}
            };

            foreach (Category item in categories)
            {
                context.Categories.Add(item);
            }

            context.SaveChanges();

            var roles = new Role[]
            {
                new Role{Code="001", Name="Administrator"},
                new Role{Code="002", Name="User"}
            };

            foreach (Role item in roles)
            {
                context.Roles.Add(item);
            }

            context.SaveChanges();

            var accounts = new Account[]
            {
                new Account{Name="Administrator", RoleID=1, Email="trungquanlai@gmail.com", PhoneNoMobile="0272951397", Password="admin", Active=true},
                new Account{Name="User1", RoleID=2, Email="trungquanlai@gmail.com", PhoneNoMobile="0272951397", Password="admin", Active=true}
            };

            foreach (Account item in accounts)
            {
                context.Accounts.Add(item);
            }

            context.SaveChanges();

            var suppliers = new Supplier[]
            {
                new Supplier {Name="Supplier 01", PhoneNoMobile="0272951397"}
            };

            foreach (Supplier item in suppliers)
            {
                context.Suppliers.Add(item);
            }

            context.SaveChanges();

            var books = new Book[]
            {
                new Book {Code="001", Name="Concert At The Hub", CategoryID=1, SupplierID=1, Price=2, Description="A musical extravaganza featuring, indie bands & acoustic acts", Image="book1.jpg"},
                new Book {Code="002", Name="Millennial Style", CategoryID=2, SupplierID=1, Price=2, Description="A musical extravaganza featuring, indie bands & acoustic acts", Image="book2.jpg"},
                new Book {Code="003", Name="The Encore Talent Show", CategoryID=3, SupplierID=1, Price=2, Description="A musical extravaganza featuring, indie bands & acoustic acts", Image="book3.jpg"},
                new Book {Code="004", Name="Mark Jason Lim", CategoryID=4, SupplierID=1, Price=2, Description="A musical extravaganza featuring, indie bands & acoustic acts", Image="book4.jpg"}
            };

            foreach (Book item in books)
            {
                context.Books.Add(item);
            }

            context.SaveChanges();
        }
    }
}
