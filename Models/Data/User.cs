using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Models.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите телефон")]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Повторите ввод пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
