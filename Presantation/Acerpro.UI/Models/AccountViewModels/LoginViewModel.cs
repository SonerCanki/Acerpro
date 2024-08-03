using System.ComponentModel.DataAnnotations;

namespace Acerpro.UI.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-Posta adresini giriniz")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parola giriniz")]
        [Display(Name = "Parola")]
        public string Password { get; set; }
    }
}
