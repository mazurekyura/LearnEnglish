using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile>
    {
        public void UpdateProfile(UserProfile userProfile, string userID);

        public UserProfile GetByUserId(long userId);
    }
}
