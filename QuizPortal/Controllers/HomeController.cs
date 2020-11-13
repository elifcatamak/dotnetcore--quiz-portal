using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPortal.Helper;
using QuizPortal.Models;
using QuizPortal.Models.Dtos;
using QuizPortal.Proxies;

namespace QuizPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWiredProxy _wiredProxy;

        public HomeController(IWiredProxy wiredProxy)
        {
            _wiredProxy = wiredProxy;
        }

        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetString(Constants.SessionUserId) == null)
            {
                return Redirect(Url.Action("Login", "User"));
            }

            var articleList = await _wiredProxy.GetLastFiveArticlesAsync();

            var quizFormDto = new QuizFormDto();
            quizFormDto.ArticleList = articleList;

            return View(quizFormDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
