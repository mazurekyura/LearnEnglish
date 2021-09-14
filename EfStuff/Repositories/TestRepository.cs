using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(LearnEnglishDbContext learnEnglishDbContext)
            : base(learnEnglishDbContext)
        {
        }               

        public List<Test> FindTestById(List<long> ids)
        {
            return _dbSet
                 .Where(x => ids.Contains(x.Id))
                 .ToList();
        }
    }
}