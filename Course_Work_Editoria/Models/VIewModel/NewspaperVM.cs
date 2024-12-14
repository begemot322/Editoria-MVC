using Course_Work_Editoria.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course_Work_Editoria.Models.VIewModel
{
    public class NewspaperVM
    {
        public Newspaper Newspaper { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Editors { get; set; }
    }
}
