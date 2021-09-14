using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories
{
    public class BankCardRepository : BaseRepository<BankCard>, IBankCardRepository
    {
        public BankCardRepository(LearnEnglishDbContext learnEnglishDbContext) 
            : base(learnEnglishDbContext)
        {
        }

        public bool Exist(string cardNumber)
        {
            return _dbSet.Any(x => x.CardNumber == cardNumber);
        }

        public List<BankCard> AllWithPage(int page, int perpage)
        {
            return _dbSet.Skip((page - 1) * perpage)
                .Take(perpage)
                .ToList();
        }

        public List<BankCard> GetByUserId(long userId)
        {
            return _dbSet.Where(x => x.OwnerId == userId).ToList();
        }
    }
}