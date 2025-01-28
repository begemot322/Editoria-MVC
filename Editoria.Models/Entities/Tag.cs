using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Models.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = "Без описания";
        [Required]
        [Range(1,5, ErrorMessage = "Приоритет должен быть от 1 до 5.")]
        public int Popularity { get; set; }

        // Связь многие-ко-многим со статьями
        [ValidateNever]
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
