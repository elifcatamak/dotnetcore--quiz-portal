using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPortal.Helper;
using QuizPortal.Models.Dtos;
using QuizPortal.Proxies;

namespace QuizPortal.Controllers
{
    public class QuizController : Controller
    {
        private readonly IWiredProxy _wiredProxy;

        public QuizController(IWiredProxy wiredProxy)
        {
            _wiredProxy = wiredProxy;
        }

        public async Task<IActionResult> CreateQuiz()
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            var articleList = await _wiredProxy.GetLastFiveArticlesAsync();

            var quizFormDto = new QuizFormDto();
            quizFormDto.ArticleList = articleList;

            return View(quizFormDto);
        }
    }
}
