using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizPortal.Models.Dtos
{
    public class QuizViewDto
    {
        [Required]
        public QuizDto QuizDto { get; set; }

        [Required]
        public List<QuestionDto> QuestionDtoList { get; set; }
    }
}
