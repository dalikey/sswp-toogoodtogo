using System.ComponentModel.DataAnnotations;

namespace Portal.Models {
    public class LoginModel {
        [Required(ErrorMessage = "Naam is vereist")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Wachtwoord is vereist")]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}