using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface IBaseRepository<Model> where Model : BaseModel
    {
        public Model Get(long id);
        public List<Model> GetAll();
        public void Save(Model model);
        public void Remove(Model model);
        public void Remove(long id);
        public int Count();
    }
}