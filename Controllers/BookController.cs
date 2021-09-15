using AutoMapper;
using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories.IRepository;
using LearnEnglish.Models.Book;
using LearnEnglish.Models.Lesson;
using LearnEnglish.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class BookController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public BookController(IMapper mapper, IUserRepository userRepository,
            IUserService userService, IBookRepository bookRepository, IFileService fileService)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userService = userService;
            _fileService = fileService;
            _bookRepository = bookRepository;
        }

        public IActionResult Books()
        {
            var viewModels = _mapper.Map<List<BookViewModel>>(_bookRepository.GetAll());
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
            book.Creater = _userService.GetCurrent();
            _bookRepository.Save(book);

            var path = _fileService.GetBookPath(book.Id);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                bookViewModel.BookFile.CopyTo(fileStream);
            }

            book.Url = _fileService.GetBookUrl(book.Id);
            _bookRepository.Save(book);

            return RedirectToAction("Books");
        }

        public IActionResult Remove(long id)
        {
            _bookRepository.Remove(id);

            System.IO.File.Delete(_fileService.GetBookPath(id));

            return RedirectToAction("Books");
        }

        public IActionResult IsUniq(string name)
        {            
            var isUniq = !_bookRepository.Exist(name);
            return Json(isUniq);
        }
    }
}
