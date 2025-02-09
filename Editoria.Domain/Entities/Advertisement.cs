using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Domain.Entities
{
    public class Advertisement
    {
        [Key]
        public int AdvertisementId { get; set; }
        [Required(ErrorMessage = "Тип объявления обязателен.")]
        [StringLength(50, ErrorMessage = "Тип объявления не может быть длиннее 50 символов.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Текст объявления обязателен.")]
        [StringLength(500, ErrorMessage = "Текст объявления не может быть длиннее 500 символов.")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Укажите стоимость.")]
        [Range(0.01, 100000, ErrorMessage = "Стоимость должна быть в диапазоне от 0.01 до 100000.")]
        public decimal Cost { get; set; }

        // Связь с выпуском
        public int IssueId { get; set; }
        [ValidateNever]
        public Issue Issue { get; set; }

    }
}
