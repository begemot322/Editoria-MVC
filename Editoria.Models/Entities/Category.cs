using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Models.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(35)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Максимальная длина 100")]
        public string Description { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Приоритет должен быть от 1 до 5.")]
        public int Priority { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Связь со статьями
        [ValidateNever]
        public ICollection<Article> Articles { get; set; }
    }
}
