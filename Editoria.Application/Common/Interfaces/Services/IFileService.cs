using Microsoft.AspNetCore.Http;

namespace Course_Work_Editoria.Services.File
{
    public interface IFileService
    {
        void DeleteFile(string relativePath);
        string SaveFile(IFormFile file, string folder);
    }
}