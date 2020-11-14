using Microsoft.EntityFrameworkCore;
using QuizPortal.Data;
using QuizPortal.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizPortal.Repositories.Sqlite
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _db;

        public QuestionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateQuestionAsync(Question question)
        {
            await _db.Questions.AddAsync(question);
        }

        public async Task<ICollection<Question>> GetAllQuestionsAsync(int quizId)
        {
            return await _db.Questions.Where(q => q.QuizId == quizId).ToListAsync();
        }
    }
}
