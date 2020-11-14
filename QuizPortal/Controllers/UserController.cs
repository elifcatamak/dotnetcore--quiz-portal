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
    public class UserController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;

        public UserController(IRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "User register validation failed" });
            }

            IUserRepository userRepository = _repositoryFactory.GetUserRepository();

            if (await userRepository.UserExistsAsync(userDto.Username))
            {
                return Json(new { success = false, message = $"User Already Exists: {userDto.Username}" });
            }

            User user = _mapper.Map<User>(userDto);

            await userRepository.CreateUserAsync(user);
            await _repositoryFactory.SaveAsync();

            return Json(new { success = true, message = "Register successful", url = Url.Action("Login", "User") });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "User login validation failed" });
            }

            IUserRepository userRepository = _repositoryFactory.GetUserRepository();

            if (!await userRepository.UserExistsAsync(userDto.Username, userDto.Password))
            {
                return Json(new { success = false, message = "Username or password is incorrect" });
            }

            var userFromDb = await userRepository.GetUserAsync(userDto.Username);

            HttpContext.Session.SetString(Constants.SessionUserId, userFromDb.Id.ToString());

            return Json(new { success = true, message = "Login successful", url = Url.Action("Index", "Home") });
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString(Constants.SessionUserId) != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Constants.SessionUserId);

            return RedirectToAction("Login", "User");
        }
    }
}
