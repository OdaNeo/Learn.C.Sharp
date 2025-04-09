using System.ComponentModel.DataAnnotations;

namespace learn_c_sharp.Dtos
{
    public class LoginDto
    {
        [Required]
        public required string Email { set; get; }
        [Required]
        public required string Password { set; get; }
    }
}
