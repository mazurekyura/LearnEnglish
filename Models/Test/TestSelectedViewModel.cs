using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.Test
{
    public class TestSelectedViewModel
    {
        public long Id { get; set; }

        public string Question { get; set; }

        public string AnswerTrue { get; set; }

        public string AnswerFalse { get; set; }

        public string AnswerFasleSecond { get; set; }

        public string AnswerFasleThird { get; set; }

        public bool IsSelectAnswerTrue { get; set; }

        public bool IsSelectAnswerFalse { get; set; }

        public bool IsSelectAnswerFalseSecond { get; set; }

        public bool IsSelectAnswerFalseThird { get; set; }

    }
}