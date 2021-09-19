using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Controllers
{
    public class GeneralController : Controller
    {
        public GeneralController()
        {

        }

        [HttpGet]
        public IActionResult Video()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Audio()
        {
            return View();
        }
    }
}
