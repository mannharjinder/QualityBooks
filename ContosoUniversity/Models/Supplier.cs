using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    //[AtLeastOneProperty("PhoneNoHome", "PhoneNoWork", "PhoneNoMobile", ErrorMessage = "You must supply at least one value")]
    public class Supplier
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }
        //[ClassicSupplier("PhoneNoHome", "PhoneNoWork", "PhoneNoMobile", ErrorMessage = "You must supply at least one value")]
        //[AtLeastOneProperty("PhoneNoHome", "PhoneNoWork", "PhoneNoMobile", ErrorMessage = "You must supply at least one value")]
        public string PhoneNoHome { get; set; }
        public string PhoneNoWork { get; set; }
        public string PhoneNoMobile { get; set; }
        public string Address { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
