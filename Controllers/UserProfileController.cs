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
using System.IO;
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
        private readonly IFileService _fileService;

        public UserProfileController(IMapper mapper,
            IUserProfileRepository userProfileRepository, IUserRepository userRepository,
            IUserService userService, IBankCardRepository bankCardRepository, IFileService fileService)
        {
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
            _userService = userService;
            _bankCardRepository = bankCardRepository;
            _fileService = fileService;
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
            var userProfile = _mapper.Map<UserProfile>(viewModel);
            userProfile.Owner = _userService.GetCurrent();
            _userProfileRepository.Save(userProfile);

            var path = _fileService.GetAvatarPath(userProfile.Id);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                viewModel.AvatarFile.CopyTo(fileStream);
            }

            userProfile.AvatarUrl = _fileService.GetAvatarUrl(userProfile.Id);
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