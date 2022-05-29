using System.ComponentModel.DataAnnotations;

namespace Model.Dto
{
    public class LoginInput
    {
        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }
    }
}