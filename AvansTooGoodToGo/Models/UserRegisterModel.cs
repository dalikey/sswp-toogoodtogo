using System.ComponentModel.DataAnnotations;

namespace Portal {
    public class UserRegisterModel {
        [Required(ErrorMessage = "Naam is vereist")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-mail is vereist")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Wachtwoord is vereist")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Wachtwoord bevestigen is vereist")]
        [Compare("Password", ErrorMessage = "Het ingevoerde wachtwoord komt niet overeen met het wachtwoord")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
