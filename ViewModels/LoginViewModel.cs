using System.ComponentModel.DataAnnotations;

namespace FabrykaBiznesuV2.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Adres email")]
        [Required(ErrorMessage = "Podaj email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj hasło!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
