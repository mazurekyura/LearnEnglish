using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.Book
{
    public class BookViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
        
        public IFormFile BookFile { get; set; }
    }
}