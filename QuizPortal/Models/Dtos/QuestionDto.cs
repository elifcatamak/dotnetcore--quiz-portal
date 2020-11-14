using System.ComponentModel.DataAnnotations;

namespace QuizPortal.Models.Dtos
{
    public class QuestionDto
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        public string AnswerA { get; set; }

        [Required]
        public string AnswerB { get; set; }

        [Required]
        public string AnswerC { get; set; }

        [Required]
        public string AnswerD { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }
    }
}
