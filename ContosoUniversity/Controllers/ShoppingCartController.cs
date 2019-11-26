using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.ShoppingCartViewModels;

namespace ContosoUniversity.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext _context;
        public int? ItemCount;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            // Return the view
            return View(cart);
        }

        public IActionResult Default()
        {
            var shopppingCart = ShoppingCart.GetCart(this.HttpContext);
            var cartItem = shopppingCart.GetCartItems(_context);
            var count = shopppingCart.GetCount(_context);
            var subTotal = shopppingCart.GetTotal(_context);
            var gst = (subTotal * 15) / 100;
            var grandTotal = subTotal + gst;

            var cart = new ShoppingCartViewModel()
            {
                CartItems = cartItem,
                CartTotal = count,
                SubTotal = subTotal,
                GST = gst,
                GrandTotal = grandTotal
            };
            return View(cart);
        }
        //
        // GET: /Store/AddToCart/5
        public void AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedBook = _context.Books
                .SingleOrDefault(book => book.ID == id);
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedBook, _context);
            // Go back to the main store page for more shopping
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult AddToCartButton(int id)
        {
            // Retrieve the album from the database
            var addedBook = _context.Books
                .SingleOrDefault(book => book.ID == id);
            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedBook, _context);
            // Go back to the main store page for more shopping
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int itemCount = cart.RemoveFromCart(id, _context);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult RemoveFromCartBtn(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.RemoveFromCartBtn(id, _context);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult EmptyCart()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.EmptyCart(_context);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public int ShoppingCartItem()
        {
            var shopppingCart = ShoppingCart.GetCart(this.HttpContext);
            var count = shopppingCart.GetCount(_context);

            return count;
        }

    }
}