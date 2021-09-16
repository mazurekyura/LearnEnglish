using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(LearnEnglishDbContext learnEnglishDbContext)
            : base(learnEnglishDbContext)
        {
        }

        public bool Exist(string lessonName)
        {
            return _dbSet.Any(x => x.Name == lessonName);
        }

        public List<Lesson> FindLessonsById(List<long> ids)
        {
            return _dbSet
                 .Where(x => ids.Contains(x.Id))
                 .ToList();
        }
    }
}