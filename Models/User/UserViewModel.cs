using LearnEnglish.EfStuff.Model;
using LearnEnglish.Models.Lesson;
using LearnEnglish.Models.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.User
{
    public class UserViewModel
    {
        public string Login { get; set; }

        public Language Language { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}