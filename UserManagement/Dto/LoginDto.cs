using System.ComponentModel.DataAnnotations;

namespace Demo.Dto
{
    public class LoginDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
