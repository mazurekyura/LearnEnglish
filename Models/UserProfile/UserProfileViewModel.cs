using LearnEnglish.Models.BankCard;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.UserProfile
{
    public class UserProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string AvatarUrl { get; set; }

        public IFormFile AvatarFile { get; set; }

        public long OwnerId { get; set; }

        public List<BankCardGetViewModel> BankCardGetViewModels { get; set; }
    }
}