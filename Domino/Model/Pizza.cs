using System.ComponentModel.DataAnnotations;

namespace Domino.Model
{
    public class Pizza
    {
        [Key]
        [Display(Name = "Pizza ID")]
        public int PizzaID { get; set; }
        [Display(Name = "Pizza Name")]
        public string? PizzaName { get; set; }
        public float? Price { get; set; }    
        public int? Stock { get; set; }  
        public List<Cart>? cart { get; set; }
        public List<Receipt>? Receipt { get; set; }
    }
}
