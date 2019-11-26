using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName ="money")]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }

        //public ShoppingCart ShoppingCart { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
