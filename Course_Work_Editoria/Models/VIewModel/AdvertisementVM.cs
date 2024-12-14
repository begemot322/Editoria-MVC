using Course_Work_Editoria.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course_Work_Editoria.Models.VIewModel
{
    public class AdvertisementVM
    {
        public Advertisement Advertisement { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Issues { get; set; }
    }
}
