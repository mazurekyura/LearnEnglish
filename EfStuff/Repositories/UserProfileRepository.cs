using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(LearnEnglishDbContext learnEnglishDbContext)
            : base(learnEnglishDbContext)
        {
        }
    }
}