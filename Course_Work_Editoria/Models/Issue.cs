using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Course_Work_Editoria.Models
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        [MaxLength(500)]  // Максимальная длина для текста
        public string Content { get; set; }

        // Связь с газетой
        public int NewspaperId { get; set; }
        [ValidateNever]
        public Newspaper Newspaper { get; set; }

        // Связь со статьями
        [ValidateNever]
        public List<Article> Articles { get; set; }

        //Связь с рекламами 
        [ValidateNever]
        public List<Advertisement> Advertisements { get; set; }

    }
}
