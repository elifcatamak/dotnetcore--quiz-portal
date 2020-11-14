using Microsoft.EntityFrameworkCore;
using QuizPortal.Data;
using QuizPortal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizPortal.Repositories.Sqlite
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _db;

        public QuizRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateQuizAsync(Quiz quiz)
        {
            await _db.Quizzes.AddAsync(quiz);
        }

        public async Task<Quiz> GetQuizAsync(int id)
        {
            return await _db.Quizzes.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<ICollection<Quiz>> GetAllQuizzesAsync()
        {
            return await _db.Quizzes.ToListAsync();
        }

        public void DeleteQuiz(Quiz quiz)
        {
            _db.Quizzes.Remove(quiz);
        }
    }
}
