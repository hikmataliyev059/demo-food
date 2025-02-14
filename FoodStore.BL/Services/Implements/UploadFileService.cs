using FoodStore.BL.Helpers.DTOs.File;
using FoodStore.BL.Helpers.Extensions;
using FoodStore.BL.Helpers.Extensions.File;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace FoodStore.BL.Services.Implements;

public class UploadFileService : IUploadFileService
{
    private readonly IWebHostEnvironment _env;

    public UploadFileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<GetUploadFileDto> UploadFileAsync(CreateUploadFileDto fileUploadDto)
    {
        string imgUrl = await fileUploadDto.File.UploadAsync(_env.WebRootPath, fileUploadDto.FolderName);
        return new GetUploadFileDto { ImgUrl = imgUrl };
    }

    public Task<bool> DeleteFileAsync(string filePath)
    {
        return Task.FromResult(FileUploadExtensions.Delete(_env.WebRootPath, filePath));
    }
}