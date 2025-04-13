using System.ComponentModel.DataAnnotations;

namespace learn_c_sharp.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<LineItem> ShoppingCartItems { get; set; }
    }
}
