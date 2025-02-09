using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Editoria.Domain.Entities
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        [MaxLength(500)]
        public string Information { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int Circulation { get; set; }

        // Связь с газетой
        public int NewspaperId { get; set; }
        [ValidateNever]
        public Newspaper Newspaper { get; set; }


        // Связь со статьями
        [ValidateNever]
        public ICollection<Article> Articles { get; set; }

        //Связь с рекламами 
        [ValidateNever]
        public ICollection<Advertisement> Advertisements { get; set; }

    }
}
