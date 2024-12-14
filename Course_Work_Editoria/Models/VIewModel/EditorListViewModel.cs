using Course_Work_Editoria.Models;

namespace Course_Work_Editoria.Models.VIewModel
{
    public class EditorListViewModel
    {
        public IEnumerable<Editor> Editors { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
