using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learn_c_sharp.Models
{
    public class LineItem // “一行商品记录” 把“一个购物车/订单里有多件不同商品”拆成若干行，主表只管谁的购物车，行项目各自管商品明细。
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TouristRouteId")]
        public Guid TouristRouteId { get; set; }
        public TouristRoute TouristRoute { get; set; }
        public Guid? ShoppingCartId { get; set; }
        //public Guid? OrderId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OriginalPrice { get; set; }
        [Range(0.0, 1.0)]
        public double? DiscountPresent { get; set; }
    }
}
