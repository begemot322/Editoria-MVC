using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.DTOs.Authentication
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "Введите текущий пароль.")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Подтвердите новый пароль.")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmNewPassword { get; set; }
    }
}
