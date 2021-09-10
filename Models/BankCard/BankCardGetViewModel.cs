using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.BankCard
{
    public class BankCardGetViewModel
    {
        public long Id { get; set; }

        public string CardNumber { get; set; }

        public int ValidityMonth { get; set; }

        public int ValidityYear { get; set; }
    }
}
