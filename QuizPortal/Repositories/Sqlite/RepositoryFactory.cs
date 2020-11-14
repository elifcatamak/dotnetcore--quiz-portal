using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuizPortal.Data;
using System.Data;
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

        public IQuestionRepository GetQuestionRepository()
        {
            return new QuestionRepository(_db);
        }

        public IQuizRepository GetQuizRepository()
        {
            return new QuizRepository(_db);
        }

        public IUserRepository GetUserRepository()
        {
            return new UserRepository(_db);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            return (await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted)).GetDbTransaction();
        }
    }
}
