using QuizPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizPortal.Repositories
{
    public interface IQuestionRepository
    {
        Task CreateQuestionAsync(Question question);

        Task<ICollection<Question>> GetAllQuestionsAsync(int quizId);
    }
}
