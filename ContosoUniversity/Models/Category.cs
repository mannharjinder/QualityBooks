using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
                
        public ICollection<Book> Books { get; set; }
    }
}
