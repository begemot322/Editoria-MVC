using Editoria.Models.Entities;

namespace Editoria.Models.ViewModel
{
    public class EditorFilterVM
    {
        public IEnumerable<Editor> Editors { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
