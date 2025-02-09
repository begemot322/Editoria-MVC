using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Editoria.Web.ViewModel
{
    public class IssueVM
    {
        public Issue Issue { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Newspapers { get; set; }
    }
}
