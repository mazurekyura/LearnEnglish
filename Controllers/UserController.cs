using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.EfStuff.Repositories.IRepository;
using LearnEnglish.Models;
using LearnEnglish.Models.Lesson;
using LearnEnglish.Models.User;
using LearnEnglish.Services;
using LearnEnglish.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserRepository userRepository,
            IUserService userService, ILessonRepository lessonRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
            _lessonRepository = lessonRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var url = Request.Query["ReturnUrl"].FirstOrDefault();
            var viewModel = new RegistrationViewModel()
            {
                ReturnUrl = url
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegistrationViewModel viewModel)
        {
            var user = _userRepository.Get(viewModel.Login, viewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(nameof(RegistrationViewModel.Login),
                    "Неправильный логин или пароль");
                return View(viewModel);
            }

            await HttpContext.SignInAsync(_userService.GetPrincipal(user));

            if (string.IsNullOrEmpty(viewModel.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(viewModel.ReturnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (_userRepository.Get(viewModel.Login) == null)
            {
                var user = _mapper.Map<User>(viewModel);
                _userRepository.Save(user);

                await HttpContext.SignInAsync(_userService.GetPrincipal(user));

                return RedirectToAction("Profile", "User");
            }
            else
            {
                ModelState.AddModelError(nameof(RegistrationViewModel.Login),
                    "Пользователь с указанным логином уже существует");
                return View(viewModel);
            }
        }

        public JsonResult IsUniq(string name)
        {
            var isUniq = !_userRepository.Exist(name);

            return Json(isUniq);
        }

        [HttpPost]
        public IActionResult SelectedLessons(List<LessonSelectedViewModel> selectedLessons)
        {
            var user = _userService.GetCurrent();

            var selectedLessonsId = selectedLessons
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            user.Lessons.RemoveRange(0, user.Lessons.Count);
            user.Lessons = _lessonRepository.FindCoursesById(selectedLessonsId);
            _userRepository.Save(user);

            return RedirectToAction("SelectedLessons");
        }

        [HttpGet]
        public IActionResult SelectedLessons()
        {
            var user = _userService.GetCurrent();

            var viewModel = new UserLessonViewModel
            {
                Login = user.Login,
                SelectedLessons = user.Lessons.Select(x => new LessonViewModel
                {
                    LessonName = x.Name,
                    Id = x.Id
                }).ToList()
            };

            return View(viewModel);
        }

        public IActionResult Denied(string returnUrl)
        {
            var user = _userService.GetCurrent();
            var viewModel = new DeniedViewModel()
            {
                DeniedUrl = returnUrl,
                RequestTime = DateTime.Now,
                UserName = user?.Login ?? "Guest"
            };
            return View(viewModel);
        }
    }
}