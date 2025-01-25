using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Editoria.Models.ViewModel
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
        [ValidateNever]
        public List<SelectListItem> Tags { get; set; }
        public List<int> SelectedTags { get; set; }

    }
}
