using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "User register validation failed" });
            }

            IUserRepository userRepository = _repositoryFactory.GetUserRepository();

            if (await userRepository.UserExistsAsync(userCreateDto.Username))
            {
                return Json(new { success = false, message = $"User Already Exists: {userCreateDto.Username}" });
            }

            User user = _mapper.Map<User>(userCreateDto);

            await userRepository.CreateUserAsync(user);
            await _repositoryFactory.SaveAsync();

            return Json(new { success = true, message = "Register successful", url = Url.Action("Login", "User") });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
