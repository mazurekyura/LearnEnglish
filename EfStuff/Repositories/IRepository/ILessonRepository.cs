using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        public bool Exist(string lessonName);

        public List<Lesson> FindCoursesById(List<long> ids);
    }
}
