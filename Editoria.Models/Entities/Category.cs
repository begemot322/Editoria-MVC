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
        [DisplayName("Имя")]
        [MaxLength(25)]
        public string Name { get; set; }
        [DisplayName("Описание")]
        [MaxLength(100, ErrorMessage = "Максимальная длина 100")]
        public string Description { get; set; }

        // Связь со статьями
        [ValidateNever]
        public ICollection<Article> Articles { get; set; }
    }
}
