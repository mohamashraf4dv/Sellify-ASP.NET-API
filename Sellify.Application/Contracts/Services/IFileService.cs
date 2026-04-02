using Microsoft.AspNetCore.Http;

namespace Sellify.Application.Contracts.Services
{
    public interface IFileService
    {
        public  Task<string> UploadFile(IFormFile file, string folderName = "Images");

    }
}
