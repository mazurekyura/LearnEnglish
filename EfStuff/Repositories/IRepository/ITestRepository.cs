using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface ITestRepository : IBaseRepository<Test>
    {
        public List<Test> FindTestById(List<long> ids);
    }
}
