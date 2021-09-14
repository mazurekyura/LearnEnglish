using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Model
{
    public class Test : BaseModel
    {
        public string Question { get; set; }

        public string AnswerTrue { get; set; }

        public string AnswerFasle { get; set; }
    }
}
