using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        public UserProfileRepository(LearnEnglishDbContext learnEnglishDbContext)
            : base(learnEnglishDbContext)
        {
        }
    }
}