using static System.Net.Mime.MediaTypeNames;
namespace Sellify.Infrastructure.Services
{
    public class FileService:IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            this._environment = environment;
        }
        public async Task<string> UploadFile(IFormFile file, string folderName="Images")
        {
            // Implement file upload logic here
            string uploadedFolder = Path.Combine(_environment.WebRootPath, "Images");
            if (!Directory.Exists(uploadedFolder))
            {
                Directory.CreateDirectory(uploadedFolder);
            }
            string fileName = $"{Guid.NewGuid().ToString()} {Path.GetExtension(file.FileName)}";
            string filePath = Path.Combine(uploadedFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"{folderName}/{fileName}";
        }
    }
}
