using Microsoft.AspNetCore.Http;

namespace FoodStore.BL.Helpers.DTOs.File;

public record CreateUploadFileDto
{
    public IFormFile File { get; set; }
    public string FolderName { get; set; }
}