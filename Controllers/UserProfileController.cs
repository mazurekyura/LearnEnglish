using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using LearnEnglish.EfStuff.Repositories.IRepository;
using LearnEnglish.Models;
using LearnEnglish.Models.User;
using LearnEnglish.Models.UserProfile;
using LearnEnglish.Services;
using LearnEnglish.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBankCardRepository _bankCardRepository;
        private readonly IUserService _userService;

        public UserProfileController(IMapper mapper,
            UserProfileRepository userProfileRepository, UserRepository userRepository,
            UserService userService, BankCardRepository bankCardRepository)
        {
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
            _userService = userService;
            _bankCardRepository = bankCardRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Profile(UserProfileViewModel viewModel)
        {
            var user = _userService.GetCurrent();
            //var myBankCard = _bankCardRepository.GetByUserId(user.Id);

            var userProfile = new UserProfile()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                AvatarUrl = viewModel.AvatarUrl,
                Owner = user
            };

            //user.Courses.RemoveRange(0, user.Courses.Count);

            //user.UserProfile.Name.

            _userProfileRepository.Save(userProfile);

            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        public IActionResult MyProfile()
        {
            var user = _userService.GetCurrent();

            var viewModel = new UserProfileViewModel
            {
                FirstName = user.UserProfile.FirstName,
                LastName = user.UserProfile.LastName,
                AvatarUrl = user.UserProfile.AvatarUrl
            };

            return View(viewModel);
        }
    }
}