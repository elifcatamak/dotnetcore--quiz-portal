using System.ComponentModel.DataAnnotations;

namespace QuizPortal.Models.Dtos
{
    public class UserDto
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
