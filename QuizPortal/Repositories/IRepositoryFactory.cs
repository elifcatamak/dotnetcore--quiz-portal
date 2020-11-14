using System.Data;
using System.Threading.Tasks;

namespace QuizPortal.Repositories
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository();

        IQuestionRepository GetQuestionRepository();

        IQuizRepository GetQuizRepository();

        Task<IDbTransaction> BeginTransactionAsync();

        Task SaveAsync();
    }
}
