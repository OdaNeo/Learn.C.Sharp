using System.ComponentModel.DataAnnotations;

namespace Learn.C.Sharp.Dtos
{
    public class LoginDto
    {
        [Required]
        public required string Email { set; get; }
        [Required]
        public required string Password { set; get; }
    }
}
