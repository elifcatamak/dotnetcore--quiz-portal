using QuizPortal.Models;
using System.Threading.Tasks;

namespace QuizPortal.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);

        Task<User> GetUserAsync(string username);

        Task<User> GetUserAsync(int id);

        Task<bool> UserExistsAsync(string username);

        Task<bool> UserExistsAsync(string username, string password);
    }
}
