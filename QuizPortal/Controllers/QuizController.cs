using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPortal.Helper;
using QuizPortal.Models;
using QuizPortal.Models.Dtos;
using QuizPortal.Proxies;
using QuizPortal.Repositories;

namespace QuizPortal.Controllers
{
    public class QuizController : Controller
    {
        private readonly IWiredProxy _wiredProxy;
        private readonly IRepositoryFactory _repositoryFactory;

        public QuizController(IWiredProxy wiredProxy, IRepositoryFactory repositoryFactory)
        {
            _wiredProxy = wiredProxy;
            _repositoryFactory = repositoryFactory;
        }

        [BindProperty]
        public QuizFormDto QuizFormDto { get; set; }

        public async Task<IActionResult> CreateQuiz()
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            var articleList = await _wiredProxy.GetLastFiveArticlesAsync();

            QuizFormDto = new QuizFormDto();
            QuizFormDto.ArticleList = articleList.ToList();

            return View(QuizFormDto);
        }

        [HttpPost]
        [ActionName("CreateQuiz")]
        public async Task<IActionResult> CreateQuizPost()
        {
            QuizFormDto.ErrorMessage = null;

            if (ModelState.IsValid)
            {
                // Distinct question control
                if (QuizFormDto.QuestionArr.Select(q => q.QuestionText).Distinct().Count() != 4)
                {
                    QuizFormDto.ErrorMessage = "Questions should be unique";

                    return View(QuizFormDto);
                }

                //Distinct answers control
                foreach(var q in QuizFormDto.QuestionArr)
                {
                    if(q.AnswerA == q.AnswerB ||
                        q.AnswerA == q.AnswerC ||
                        q.AnswerA == q.AnswerD ||
                        q.AnswerB == q.AnswerC ||
                        q.AnswerB == q.AnswerD ||
                        q.AnswerC == q.AnswerD)
                    {
                        QuizFormDto.ErrorMessage = "A question cannot have the same answer more than once";

                        return View(QuizFormDto);
                    }
                }

                var transaction = await _repositoryFactory.BeginTransactionAsync();

                var quizRepository = _repositoryFactory.GetQuizRepository();

                var selectedArt = QuizFormDto.ArticleList.FirstOrDefault(a => a.Guid == QuizFormDto.SelectedArticleId);

                if (selectedArt == null)
                {
                    return View(QuizFormDto);
                }

                var quiz = new Quiz();

                quiz.Title = selectedArt.Title;
                quiz.ArticleGuid = selectedArt.Guid;

                await quizRepository.CreateQuizAsync(quiz);
                await _repositoryFactory.SaveAsync();

                var questionRepository = _repositoryFactory.GetQuestionRepository();

                foreach (var item in QuizFormDto.QuestionArr)
                {
                    var ques = new Question();

                    ques.QuestionText = item.QuestionText;
                    ques.AnswerA = item.AnswerA;
                    ques.AnswerB = item.AnswerB;
                    ques.AnswerC = item.AnswerC;
                    ques.AnswerD = item.AnswerD;
                    ques.CorrectAnswer = item.CorrectAnswer;
                    ques.QuizId = quiz.Id;

                    await questionRepository.CreateQuestionAsync(ques);
                }

                await _repositoryFactory.SaveAsync();

                transaction.Commit();

                return RedirectToAction("Index", "Home");
            }

            return View(QuizFormDto);
        }
    }
}
