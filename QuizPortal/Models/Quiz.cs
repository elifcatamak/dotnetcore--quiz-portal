using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizPortal.Models
{
    [Table("Quizzes")]
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ArticleId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
