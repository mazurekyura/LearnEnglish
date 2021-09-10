using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Model
{
    public class User : BaseModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }

        public Language Language { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual List<BankCard> BankCards { get; set; }

        public virtual List<Lesson> Lessons { get; set; }
    }
}
