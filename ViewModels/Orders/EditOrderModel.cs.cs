using AutoParts.Models.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoParts.ViewModels.Orders
{
    public class EditOrderModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]
        public short Id { get; set; }

        [Required]
        public string IdUser { get; set; }

        [Required]
        public int IdSpare { get; set; }

        [Display(Name = "Дата")]
        public DateTime DateOfReg { get; set; }
    }
}
