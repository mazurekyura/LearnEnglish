using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Services.Interfaces
{
    public interface IFileService
    {
        public string GetTempDocxFilePath();

        public string GetAvatarPath(long id);

        public string GetAvatarFolderPath();

        public string GetAvatarUrl(long id);

        public string GetBookFolderPath();

        public string GetBookPath(long id);

        public string GetBookUrl(long id);
    }
}