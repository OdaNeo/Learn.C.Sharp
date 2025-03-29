using learn_c_sharp.Dtos;
using System.ComponentModel.DataAnnotations;
namespace learn_c_sharp.ValidationAttributes
{
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var touristRouteDto = (TouristRouteForCreationDto)validationContext.ObjectInstance;
            if (touristRouteDto.Title == touristRouteDto.Description)
            {
                return new ValidationResult("not same", new[] { "TouristRouteForCreationDto" });
            }
            return ValidationResult.Success;
        }
    }
}
