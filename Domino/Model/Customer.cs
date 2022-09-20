using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domino.Model
{
    public class Customer
    {
        [Key]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }

        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Mobile Number")]
        public long MobileNumber { get; set; }

        [Display(Name = "E-mail ID")]
        [DataType(DataType.EmailAddress)]
        public string? EmailID { get; set; }

        public string? Address { get; set; }

        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
           ErrorMessage = "Password must contains one Uppercase,one Lowercase and one Specialcharacter")]
        public string? Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string? CPassword { get; set; }

        [Display(Name = "Carttype ID")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? CartTypeID { get; set; } 
        public List<Cart>? cart { get; set; }  
        public List<Payment>? Payment { get; set; }
        public List<Receipt>? Receipt { get; set; }
    }
}
