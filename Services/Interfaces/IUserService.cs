using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearnEnglish.Services.Interfaces
{
    public interface IUserService
    {
        public User GetCurrent();
        public bool IsAdmin();
        public bool IsModerator();
        public ClaimsPrincipal GetPrincipal(User user);
    }
}