using QuizPortal.Data;
using System.Threading.Tasks;

namespace QuizPortal.Repositories.Sqlite
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly AppDbContext _db;

        public RepositoryFactory(AppDbContext db)
        {
            _db = db;
        }

        public IUserRepository GetUserRepository()
        {
            return new UserRepository(_db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
