using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetTempDocxFilePath()
        {
            var fileName = $"{Guid.NewGuid()}.docx";
            return Path.Combine(_webHostEnvironment.WebRootPath, "temp", fileName);
        }

        //public string GetCatFolderPath()
        //    => Path.Combine(_webHostEnvironment.WebRootPath, "image\\cats");

        //public string GetCatPath(long catId)
        //    => Path.Combine(GetCatFolderPath(), $"{catId}.png");

        //public string GetCatUrl(long catId)
        //    => $"/image/cats/{catId}.png";
    }
}
