using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.C.Sharp.Models
{
    public class TouristRoutePicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public required string Url { get; set; }
        [ForeignKey("TouristRouteId")]
        public Guid? TouristRouteId { get; set; }
        public required TouristRoute TouristRoute { get; set; }
    }
}
