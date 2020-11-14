using QuizPortal.Models;
using System.Threading.Tasks;

namespace QuizPortal.Repositories
{
    public interface IQuizRepository
    {
        Task CreateQuizAsync(Quiz quiz);

        Task<Quiz> GetQuizAsync(int id);
    }
}
