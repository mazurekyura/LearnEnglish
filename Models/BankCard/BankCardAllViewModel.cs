using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.BankCard
{
    public class BankCardAllViewModel
    {
        public int Page { get; set; }

        public int RecordPerPage { get; set; }

        public int TotalRecordCount { get; set; }

        public int AllPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalRecordCount / RecordPerPage);
            }
        }

        public int FirstPositionOnPage
        {
            get
            {
                return (Page - 1) * RecordPerPage + 1;
            }
        }

        public int LastPositionOnPage
        {
            get
            {
                return (Page * RecordPerPage <= TotalRecordCount) ? Page * RecordPerPage : TotalRecordCount;
            }
        }

        public List<BankCardGetViewModel> BankCards { get; set; }
    }
}
