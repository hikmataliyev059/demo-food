using Microsoft.AspNetCore.Http;

namespace FoodStore.BL.Helpers.Extensions.File;

public static class FileUploadExtensions
{
    public static async Task<string> UploadAsync(this IFormFile file, string rootPath, string folderName)
    {
        string uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        string folderPath = Path.Combine(rootPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string filePath = Path.Combine(folderPath, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
    }

    public static bool Delete(string rootPath, string filePath)
    {
        var fullPath = Path.Combine(rootPath, filePath);

        if (!System.IO.File.Exists(fullPath)) return false;

        System.IO.File.Delete(fullPath);
        return true;
    }
}