using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.Models;
using LearnEnglish.Models.BankCard;
using LearnEnglish.Models.Lesson;
using LearnEnglish.Models.User;
using LearnEnglish.Models.UserProfile;
using LearnEnglish.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class LessonController : Controller
    {
        private readonly LessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        private readonly UserRepository _userRepository;
        private readonly FileService _fileService;
        private readonly UserService _userService;

        public LessonController(LessonRepository lessonRepository,
            IMapper mapper,
            UserRepository userRepository,
            FileService fileService, UserService userService)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _fileService = fileService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(LessonAddViewModel viewModel)
        {
            var newCourse = _mapper.Map<Lesson>(viewModel);
            _lessonRepository.Save(newCourse);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Select()
        {
            var userLessonsId = _userService.GetCurrent().Lessons.Select(x => x.Id).ToList();

            var viewModel = _lessonRepository.GetAll().Select(x => new LessonSelectedViewModel
            {
                LessonName = x.Name,
                Id = x.Id,
                IsSelected = userLessonsId.Contains(x.Id)
            }).ToList();

            return View(viewModel);
        }
    }
}
