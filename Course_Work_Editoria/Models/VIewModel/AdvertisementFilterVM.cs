using Microsoft.AspNetCore.Mvc.Rendering;

namespace Course_Work_Editoria.Models.VIewModel
{
    public class AdvertisementFilterVM
    {
        public IEnumerable<Advertisement> Advertisements { get; set; }
        public string TypeFilter { get; set; }
        public int? IssueFilter { get; set; }
        public List<SelectListItem> IssueSelectList { get; set; }
    }
}
