using AutoMapper;
using LearnEnglish.Controllers.AuthAttribute;
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

        [IsModerator]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [IsModerator]
        [HttpPost]
        public IActionResult Add(TestAddViewModel viewModel)
        {
            var newTest = _mapper.Map<Test>(viewModel);
            _testRepository.Save(newTest);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
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
        public IActionResult Answers(List<TestSelectedViewModel> viewModels)
        {
            var user = _userService.GetCurrent();                        
            user.NumberCorrectAnswers = viewModels
                .Count(x => x.IsSelectAnswerTrue && !x.IsSelectAnswerFalse);

            _userRepository.Save(user);

            return RedirectToAction("NumberCorrectAnswers");
        }

        [HttpGet]
        public IActionResult NumberCorrectAnswers()
        {
            var user = _userService.GetCurrent();

            var viewModel = new NumberCorrectAnswersViewModel
            {
                UserName = user.Login,
                NumberCorrectAnswers = user.NumberCorrectAnswers
            };

            return View(viewModel);
        }        
    }
}
