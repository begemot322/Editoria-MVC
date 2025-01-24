using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Models.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Некорректный формат Email.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Телефон обязателен.")]
        [Phone(ErrorMessage = "Введите корректный номер телефона.")]
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
    }
}
