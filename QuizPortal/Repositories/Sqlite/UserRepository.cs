using Microsoft.EntityFrameworkCore;
using QuizPortal.Data;
using QuizPortal.Models;
using System.Threading.Tasks;

namespace QuizPortal.Repositories.Sqlite
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateUserAsync(User user)
        {
            await _db.AddAsync(user);
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _db.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> UserExistsAsync(string username, string password)
        {
            return await _db.Users.AnyAsync(u => u.Username == username && u.Password == password);
        }
    }
}
