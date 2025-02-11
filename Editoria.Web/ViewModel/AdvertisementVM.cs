using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Editoria.Web.ViewModel
{
    public class AdvertisementVM
    {
        public Advertisement? Advertisement { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Issues { get; set; }
    }
}
