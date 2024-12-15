using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Course_Work_Editoria.Models
{
    public class Newspaper
    {
        [Key]
        public int NewspaperId { get; set; } 
        public string Name { get; set; }
        public int Circulation { get; set; }
        public string Type { get; set; }
        public string Region { get; set; }

        // Связь с редактором
        public int EditorId { get; set; }
        [ValidateNever]
        public Editor Editor { get; set; }

        // Связь с выпусками
        [ValidateNever]
        public ICollection<Issue> Issues { get; set; }
    }
}
