using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "No email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not correct")]
        public string ConfirmPassword { get; set; }
    }
}
