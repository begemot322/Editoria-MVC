using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Course_Work_Editoria.Services.File
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string SaveFile(IFormFile file, string folder)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, folder, fileName);

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return $"/{folder}/{fileName}";
        }
        public void DeleteFile(string relativePath)
        {
            string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
