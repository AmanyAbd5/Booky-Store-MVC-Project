using System.Net;

namespace Booky.PL.Helper
{
    public static class filesManagement
    {
        public static async Task<string> UploadFile(IFormFile file , string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);
            string fileName =$"{Guid.NewGuid()}{file.FileName}";
            string filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string imagePath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName,fileName);
            if(File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }
    }
}
