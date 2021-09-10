using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.Models;
using LearnEnglish.Models.User;
using LearnEnglish.Services;
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
        private readonly UserRepository _userRepository;
        private readonly LessonRepository _courseRepository;
        private readonly UserService _userService;

        public UserController(IMapper mapper, UserRepository userRepository,
            UserService userService, LessonRepository lessonRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
            _courseRepository = lessonRepository;
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
                return RedirectToAction("/");
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
        public IActionResult SelectedCourses(List<LessonSelectedViewModel> selectedCorses)
        {
            var user = _userService.GetCurrent();

            var selectedCorsesId = selectedCorses
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            user.Lessons.RemoveRange(0, user.Lessons.Count);
            user.Lessons = _courseRepository.FindCoursesById(selectedLessonsId);
            _userRepository.Save(user);

            return RedirectToAction("SelectedCourses");
        }

        [HttpGet]
        public IActionResult SelectedCourses()
        {
            var user = _userService.GetCurrent();

            var viewModel = new UserCourseViewModel
            {
                Login = user.Login,
                SelectedCourses = user.Courses.Select(x => new CourseViewModel
                {
                    CourseName = x.Name,
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
