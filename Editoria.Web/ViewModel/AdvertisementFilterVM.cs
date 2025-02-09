using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Editoria.Web.ViewModel
{
    public class AdvertisementFilterVM
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public string TypeFilter { get; set; }
        public int? IssueFilter { get; set; }
        public List<SelectListItem> IssueSelectList { get; set; }
    }
}
