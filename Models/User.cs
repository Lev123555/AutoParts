using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]

        public string NameUser { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]

        public string Pass { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [Display(Name = "Телефон")]

        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [Display(Name = "Email")]

        public string Email { get; set; }

    }
}
