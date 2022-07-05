using System.ComponentModel.DataAnnotations;

namespace Simple.Services.System.Model.User;

public class LoginInput
{
    [Required]
    public string Account { get; set; }

    [Required]
    public string Password { get; set; }
}