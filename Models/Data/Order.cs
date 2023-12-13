using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.Models.Data
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required(ErrorMessage = "Ид_пользователя")]
        [Display(Name = "Ид_пользователя")]
        public string IdUser { get; set; }

        [Required(ErrorMessage = "Ид_запчасти")]
        [Display(Name = "Ид_запчасти")]
        public short IdSpare { get; set; }

        [Required(ErrorMessage = "Введите дату")]
        [Display(Name = "Дата")]
        public DateTime DateOfReg { get; set; }

        [ForeignKey("IdUser")]
        [Display(Name = "Ид пользователя")]
        public User User { get; set; }

        [ForeignKey("IdSpare")]
        [Display(Name = "Ид запчасти")]
        public Spare Spare { get; set; }


    }
}
