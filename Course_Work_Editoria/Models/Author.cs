using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Course_Work_Editoria.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения.")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов.")]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Фамилия обязательна для заполнения.")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов.")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Электронная почта обязательна для заполнения.")]
        [EmailAddress(ErrorMessage = "Введите корректный адрес электронной почты.")]
        [DisplayName("Почта")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Номер телефона обязателен для заполнения.")]
        [Phone(ErrorMessage = "Введите корректный номер телефона.")]
        [DisplayName("Телефон")]
        public string Phone { get; set; }

        // Связь со статьями
        [ValidateNever]
        public List<Article> Articles { get; set; } 
    }
}
