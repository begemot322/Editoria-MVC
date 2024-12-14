using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Course_Work_Editoria.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PublicationDate { get; set; }

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
    }
}
