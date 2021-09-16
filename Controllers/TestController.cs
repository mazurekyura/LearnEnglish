using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.EfStuff.Repositories.IRepository;
using LearnEnglish.Models;
using LearnEnglish.Models.BankCard;
using LearnEnglish.Models.Lesson;
using LearnEnglish.Models.Test;
using LearnEnglish.Models.User;
using LearnEnglish.Models.UserProfile;
using LearnEnglish.Services;
using LearnEnglish.Services.Interfaces;
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
    public class TestController : Controller
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public TestController(ITestRepository testRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IFileService fileService, IUserService userService)
        {
            _testRepository = testRepository;
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
        public IActionResult Add(TestAddViewModel viewModel)
        {
            var newTest = _mapper.Map<Test>(viewModel);
            _testRepository.Save(newTest);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Select()
        {
            var allTests = _testRepository.GetAll();

            var viewModels = allTests.Select(x => new TestSelectedViewModel
            {
                Id = x.Id,
                Question = x.Question,
                AnswerTrue = x.AnswerTrue,
                AnswerFalse = x.AnswerFasle,
                AnswerFasleSecond = x.AnswerFasleSecond,
                AnswerFasleThird = x.AnswerFasleThird                
            }).ToList();
            
            return View(viewModels);
        }

        [HttpPost]
        public string Answers(List<TestSelectedViewModel> viewModels)
        {            
            var numberCorrectAnswers = viewModels.Count(x => x.IsSelectAnswerTrue && !x.IsSelectAnswerFalse);

            var uncorrectAnswers = viewModels.Where(x => !x.IsSelectAnswerTrue).Select(x => x.Id).ToList();




                var selectedLessonsId = selectedLessons
                .Where(x => x.IsSelected)
                .Select(x => x.Id)
                .ToList();

            return $"правильных ответов {numberCorrectAnswers}";
        }


            //[HttpPost]
            //public IActionResult Answers(List<TestSelectedViewModel> viewmodels)
            //{
            //    var user = _userService.GetCurrent();

            //    var selectedId = viewmodels
            //        .Where(x => x.)
            //        .Select(x => x.Id)
            //        .ToList();

            //    user.Lessons.RemoveRange(0, user.Lessons.Count);
            //    user.Lessons = _lessonRepository.FindCoursesById(selectedId);
            //    _userRepository.Save(user);

            //    return RedirectToAction("SelectedLessons");
            //}

            //[HttpGet]
            //public IActionResult Select()
            //{
            //    var userLessonsId = _userService.GetCurrent().Lessons.Select(x => x.Id).ToList();

            //    var viewModel = _lessonRepository.GetAll().Select(x => new LessonSelectedViewModel
            //    {
            //        LessonName = x.Name,
            //        Id = x.Id,
            //        IsSelected = userLessonsId.Contains(x.Id)
            //    }).ToList();

            //    return View(viewModel);
            //}
        }
}
