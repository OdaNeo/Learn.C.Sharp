using System.ComponentModel.DataAnnotations;

namespace Learn.C.Sharp.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "mimacuowu")]
        public string ConfirmPassword { get; set; }
    }
}
