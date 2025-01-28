using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Models.Entities
{
    public class Newspaper
    {
        [Key]
        public int NewspaperId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Circulation { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
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
