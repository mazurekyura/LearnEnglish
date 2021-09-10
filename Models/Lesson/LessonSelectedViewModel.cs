using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.Lesson
{
    public class LessonSelectedViewModel
    {
        public long Id { get; set; }

        public string LessonName { get; set; }

        public bool IsSelected { get; set; }
    }
}
