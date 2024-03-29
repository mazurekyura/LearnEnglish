﻿using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public bool Exist(string name);
    }
}
