using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoParts.ViewModels.Spares
{
    public class CreateSpareModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ИД")]

        public short Id { get; set; }

        [Required(ErrorMessage = "Введите название запчасти")]
        [Display(Name = "Запчасть")]

        public string TitleSpare { get; set; }

        [Required(ErrorMessage = "Введите описание запчасти")]
        [Display(Name = "Описание")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Введите цену запчасти")]
        [Display(Name = "Цена")]

        public string Price { get; set; }

        [Required(ErrorMessage = "Введите категорию запчасти")]
        [Display(Name = "Категория")]

        public string Category { get; set; }

        [Required(ErrorMessage = "Введите артикул запчасти")]
        [Display(Name = "Артикул")]

        public string Article { get; set; }

        [Required(ErrorMessage = "Введите марку авто")]
        [Display(Name = "Марка")]

        public string CarBrand { get; set; }

        [Required(ErrorMessage = "Введите модель авто")]
        [Display(Name = "Модель")]

        public string CarModel { get; set; }
    }
}
