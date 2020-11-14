using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizPortal.Helper;
using QuizPortal.Models;
using QuizPortal.Models.Dtos;
using QuizPortal.Repositories;

namespace QuizPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        public HomeController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString(Constants.SessionUserId);

            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            var userRepository = _repositoryFactory.GetUserRepository();
            var userFromdb = await userRepository.GetUserAsync(int.Parse(userId));

            var userDto = _mapper.Map<UserDto>(userFromdb);

            return View(userDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
