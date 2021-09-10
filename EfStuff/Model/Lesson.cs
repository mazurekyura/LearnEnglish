using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Model
{
    public class Lesson : BaseModel
    {
        public string Name { get; set; }

        public string Level { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
