using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizPortal.Models.Dtos
{
    public class QuizFormDto
    {
        [Required]
        public List<ArticleDto> ArticleList { get; set; }

        [Required]
        public string SelectedArticleId { get; set; }

        [Required]
        public QuestionDto[] QuestionArr { get; set; } = new QuestionDto[4];
    }
}
