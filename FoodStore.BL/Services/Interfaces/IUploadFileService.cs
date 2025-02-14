using FoodStore.BL.Helpers.DTOs.File;

namespace FoodStore.BL.Services.Interfaces;

public interface IUploadFileService
{
    Task<GetUploadFileDto> UploadFileAsync(CreateUploadFileDto fileUploadDto);
    Task<bool> DeleteFileAsync(string filePath);
}