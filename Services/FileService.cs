using LearnEnglish.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Services
{
    public class FileService : IFileService
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

        public string GetAvatarFolderPath()
            => Path.Combine(_webHostEnvironment.WebRootPath, "image\\avatars");

        public string GetAvatarPath(long id)
            => Path.Combine(GetAvatarFolderPath(), $"{id}.png");

        public string GetAvatarUrl(long id)
            => $"/image/avatars/{id}.png";
    }
}