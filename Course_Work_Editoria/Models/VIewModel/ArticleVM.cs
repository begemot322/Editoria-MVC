using Course_Work_Editoria.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course_Work_Editoria.Models.VIewModel
{
    public class ArticleVM
    {
        public Article Article { get; set; }
        [ValidateNever]
        public List<SelectListItem> Issues { get; set; }
        [ValidateNever]
        public List<SelectListItem> Categories { get; set; }
        [ValidateNever]
        public List<SelectListItem> Authors { get; set; }
    }
}
