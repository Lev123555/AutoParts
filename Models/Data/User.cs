using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Models.Data
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]

        public string NameUser { get; set; }
    }
}
