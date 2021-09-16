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

        public UserProfile GetByUserId(long userId)
        {
            return _dbSet
                .Where(x => x.Owner.Id == userId)
                .FirstOrDefault();
        }

        public void UpdateProfile(UserProfile userProfile, string userID)
        {
            if (userProfile.Id == 0)
            {
                _dbSet.Add(userProfile);
            }
            else
            {
                var userProfileToUpdate = _dbSet.Where(x => x.Id == userProfile.Id).FirstOrDefault();

                if (userProfileToUpdate != null)
                {
                    _learnEnglishDbContext.Entry(userProfileToUpdate).CurrentValues.SetValues(userProfile);
                }
            }

            _learnEnglishDbContext.SaveChanges();               
        }
    }
}