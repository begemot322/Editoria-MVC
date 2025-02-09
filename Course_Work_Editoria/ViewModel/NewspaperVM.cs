using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Editoria.Web.ViewModel
{
    public class NewspaperVM
    {
        public Newspaper Newspaper { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Editors { get; set; }
    }
}
