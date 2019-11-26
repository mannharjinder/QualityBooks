using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
