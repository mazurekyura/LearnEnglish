using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.UserProfile
{
    public class UserProfileUpdateViewModel
    {        
        public IFormFile AvatarFile { get; set; }
    }
}
