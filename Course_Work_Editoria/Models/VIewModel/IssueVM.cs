using Course_Work_Editoria.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course_Work_Editoria.Models.VIewModel
{
    public class IssueVM
    {
        public Issue Issue { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Newspapers { get; set; }
    }
}
