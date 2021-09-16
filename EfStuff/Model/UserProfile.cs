using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Model
{
    public class UserProfile : BaseModel
    {
        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public string Sex { get; set; }

        public DateTime BirthDate { get; set; }

        public long UserId { get; set; }

        public virtual User Owner { get; set; }
    }
}