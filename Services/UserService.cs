using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearnEnglish.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public User GetCurrent()
        {
            var idStr = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .SingleOrDefault(x => x.Type == "Id")
                ?.Value;

            if (string.IsNullOrEmpty(idStr))
            {
                return null;
            }

            var id = int.Parse(idStr);
            return _userRepository.Get(id);
        }

        public bool IsAdmin() => GetCurrent()?.Role == Role.Admin;

        public bool IsModerator() => GetCurrent()?.Role == Role.Moderator;

        public ClaimsPrincipal GetPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.AuthenticationMethod, Startup.AuthName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, Startup.AuthName);

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
