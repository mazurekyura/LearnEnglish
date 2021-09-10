using LearnEnglish.EfStuff.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public abstract class BaseRepository<Model>
        where Model : BaseModel
    {
        protected LearnEnglishDbContext _learnEnglishDbContext;
        protected DbSet<Model> _dbSet;

        protected BaseRepository(LearnEnglishDbContext learnEnglishDbContext)
        {
            _learnEnglishDbContext = learnEnglishDbContext;
            _dbSet = _learnEnglishDbContext.Set<Model>();
        }

        public Model Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public List<Model> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Save(Model model)
        {
            if (model.Id > 0)
            {
                _learnEnglishDbContext.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }

            _learnEnglishDbContext.SaveChanges();
        }

        public void Remove(Model model)
        {
            _dbSet.Remove(model);
            _learnEnglishDbContext.SaveChanges();
        }

        public void Remove(long id)
        {
            Remove(Get(id));
        }

        public int Count()
        {
            return _dbSet.Count();
        }
    }
}
