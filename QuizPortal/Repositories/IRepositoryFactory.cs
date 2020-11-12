using System.Threading.Tasks;

namespace QuizPortal.Repositories
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository();

        Task SaveAsync();
    }
}
