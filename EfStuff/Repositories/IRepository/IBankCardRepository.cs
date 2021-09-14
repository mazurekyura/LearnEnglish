using LearnEnglish.EfStuff.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff.Repositories.IRepository
{
    public interface IBankCardRepository : IBaseRepository<BankCard>
    {
        public bool Exist(string cardNumber);

        public List<BankCard> AllWithPage(int page, int perpage);

        public List<BankCard> GetByUserId(long userId);
    }
}
