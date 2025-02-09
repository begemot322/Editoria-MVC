using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Domain.Entities
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

        [Required(ErrorMessage = "Номер телефона обязателен для заполнения.")]
        [Phone(ErrorMessage = "Введите корректный номер телефона.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Биография обязательна к заполнению")]
        [StringLength(1000, ErrorMessage = "Биография не должна превышать 1000 символов.")]
        public string Biography { get; set; }

        [Required(ErrorMessage = "Дата рождения обязательна к заполнению.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Опыт работы обязателен к заполнению")]
        public string WorkExperience { get; set; }


        // Связь с газетой
        [ValidateNever]
        public Newspaper Newspaper { get; set; }
    }
}
