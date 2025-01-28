using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Models.Entities
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "Укажите название статьи")]
        [StringLength(100, ErrorMessage = "Длина названия не должна превышать 100 символов")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите текст статьи")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Укажите дату публикации")]
        [DataType(DataType.Date, ErrorMessage = "Неверный формат даты")]
        public DateTime PublicationDate { get; set; }

        [StringLength(500, ErrorMessage = "Комментарий автора не должен превышать 500 символов")]
        public string AuthorComment { get; set; } = "Без комментария";

        // Связь с выпуском
        public int IssueId { get; set; }
        [ValidateNever]
        public Issue Issue { get; set; }

        // Связь с категорией
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

        // Связь с автором
        public int AuthorId { get; set; }
        [ValidateNever]
        public Author Author { get; set; }

        // Связь многие-ко-многим с тегами
        [ValidateNever]
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
