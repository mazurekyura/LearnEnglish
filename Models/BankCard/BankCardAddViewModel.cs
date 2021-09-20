﻿using LearnEnglish.Models.CustomValidationAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.BankCard
{
    [BankCardExpiration]
    public class BankCardAddViewModel
    {
        [BankCardNumber]
        public string CardNumber { get; set; }

        public int ValidityMonth { get; set; }

        public int ValidityYear { get; set; }

        public long OwnerId { get; set; }
    }
}