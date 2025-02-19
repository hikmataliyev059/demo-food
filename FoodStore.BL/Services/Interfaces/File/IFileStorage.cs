using FoodStore.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace FoodStore.BL.Services.Interfaces.File;

public interface IFileStorage
{
    Task<string> UploadFileAsync(IFormFile file, StorageContainer container);
    
    string GetFileUrl(string fileName, StorageContainer container);
    
    Task DeleteFileAsync(string fileName, StorageContainer container);
}