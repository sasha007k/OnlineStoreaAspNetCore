using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "No email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
