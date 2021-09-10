using LearnEnglish.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.User
{
    public class UserLessonViewModel
    {
        public string Login { get; set; }

        public List<LessonViewModel> SelectedLessons { get; set; }
    }
}
