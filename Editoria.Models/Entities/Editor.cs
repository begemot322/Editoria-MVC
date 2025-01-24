using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Models.Entities
{
    public class Editor
    {
        [Key]
        public int EditorId { get; set; }

        [Required(ErrorMessage = "Имя обязательно.")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Фамилия обязательна.")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        [StringLength(100, ErrorMessage = "Email не должен превышать 100 символов.")]
        public string Email { get; set; }

        // Связь с газетой
        [ValidateNever]
        public Newspaper Newspaper { get; set; }
    }
}
