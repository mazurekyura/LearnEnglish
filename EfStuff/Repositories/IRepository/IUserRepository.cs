using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User Get(string login, string password);

        public User Get(string login);

        public bool Exist(string login);
    }
}
