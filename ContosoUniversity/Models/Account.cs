using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    //[AtLeastOneProperty("PhoneNoHome", "PhoneNoWork", "PhoneNoMobile", ErrorMessage = "You must supply at least one value")]
    public class Account
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }
        public string PhoneNoHome { get; set; }
        public string PhoneNoWork { get; set; }
        public string PhoneNoMobile { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleID { get; set; }

        public Role Role { get; set; }
                
        public Account()
        {
            this.Active = true;
        }
    }
}
