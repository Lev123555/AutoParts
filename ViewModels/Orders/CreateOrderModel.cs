using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoParts.ViewModels.Orders
{
    public class CreateOrderModel
    {
        [Required]
        public string IdUser { get; set; }

        [Required]
        public short IdSpare { get; set; }

        [Display(Name = "Дата")]
        public DateTime DateOfReg { get; set; }
    }
}
