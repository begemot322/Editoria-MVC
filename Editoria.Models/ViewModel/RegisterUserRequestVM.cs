using Editoria.Models.Requests;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Models.ViewModel
{
    public class RegisterUserRequestVM
    {
        public RegisterUserRequest RegisterUserRequest { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
