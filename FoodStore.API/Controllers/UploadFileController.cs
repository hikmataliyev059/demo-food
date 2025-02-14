using FoodStore.BL.Helpers.DTOs.File;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadFileController : ControllerBase
{
    private readonly IUploadFileService _fileUploadService;

    public UploadFileController(IUploadFileService fileUploadService)
    {
        _fileUploadService = fileUploadService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] CreateUploadFileDto fileUploadDto)
    {
        GetUploadFileDto fileResponse = await _fileUploadService.UploadFileAsync(fileUploadDto);
        return Ok(fileResponse);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteFile([FromQuery] string filePath)
    {
        await _fileUploadService.DeleteFileAsync(filePath);
        return Ok(new { Message = "File deleted successfully" });
    }
}