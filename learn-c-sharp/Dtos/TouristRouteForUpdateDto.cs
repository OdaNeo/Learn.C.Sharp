using System.ComponentModel.DataAnnotations;

namespace learn_c_sharp.Dtos
{
    public class TouristRouteForUpdateDto
    {
        [Required(ErrorMessage = "title bukeweikong")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }
        //public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public double? DiscountPresent { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? DepartureTime { get; set; }
        public string Features { get; set; }
        public string Fees { get; set; }
        public string Notes { get; set; }
        public double Rating { get; set; }
        public string? TravelDays { get; set; }
        public string? TripType { get; set; }
        public string? DepartureCity { get; set; }
        public ICollection<TouristRoutePictureFroCreationDto> TouristRoutePictures { get; set; }
      = new List<TouristRoutePictureFroCreationDto>();
    }
}
