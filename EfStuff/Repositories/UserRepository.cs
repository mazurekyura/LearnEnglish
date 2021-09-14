using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LearnEnglishDbContext dbContext)
            : base(dbContext)
        {
        }

        public User Get(string login, string password)
        {
            return _dbSet
                .SingleOrDefault(x => x.Login == login && x.Password == password);
        }

        public User Get(string login)
        {
            return _dbSet
                .SingleOrDefault(x => x.Login == login);
        }

        public bool Exist(string login)
        {
            return _dbSet.Any(x => x.Login == login);
        }
    }
}