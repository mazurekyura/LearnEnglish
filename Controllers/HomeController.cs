using LearnEnglish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //test
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //private IHostingEnvironment Environment;

        //public HomeController(IHostingEnvironment _environment)
        //{
        //    Environment = _environment;
        //}

        //public IActionResult Music()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Music(IFormFile postedFile)
        //{
        //    string path = Path.Combine(this.Environment.WebRootPath, "Uploads");

        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    string fileName = Path.GetFileName(postedFile.FileName);
        //    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        //    {
        //        postedFile.CopyTo(stream);
        //    }

        //    ViewBag.Data = "data:audio/wav;base64," + Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(path, fileName)));
        //    return View();
        //}
    }
}
