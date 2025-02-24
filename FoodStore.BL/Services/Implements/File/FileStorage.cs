using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FoodStore.BL.Services.Interfaces.File;
using FoodStore.Core.Enums;
using FoodStore.Core.Enums.AzureContainer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FoodStore.BL.Services.Implements.File;

public class FileStorage : IFileStorage
{
    private readonly string? _connectionString;

    public FileStorage(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("AzureBlobStorage:ConnectionString");

        Console.WriteLine($"Azure Connection String: {_connectionString}");

        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new ArgumentException("Azure Blob Storage connection string is not configured.");
        }
    }

    public async Task<string> UploadFileAsync(IFormFile file, StorageContainer container)
    {
        var containerName = container.ToString().ToLower();

        var blobServiceClient = new BlobServiceClient(_connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

        string fileName = file.FileName;
        string guid = Guid.NewGuid().ToString();

        if (fileName.Length > 28) fileName = fileName.Substring(fileName.Length - 28);

        fileName = Guid.NewGuid() + fileName;

        var blobClient = blobContainerClient.GetBlobClient(fileName);

        var options = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            }
        };

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, options);
        }

        return fileName;
    }


    public string GetFileUrl(string fileName, StorageContainer container)
    {
        var containerName = container.ToString().ToLower();

        var blobServiceClient = new BlobServiceClient(_connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(fileName);

        return blobClient.Uri.AbsoluteUri;
    }

    public async Task DeleteFileAsync(string fileName, StorageContainer container)
    {
        var containerName = container.ToString().ToLower();

        var blobServiceClient = new BlobServiceClient(_connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(fileName);

        await blobClient.DeleteIfExistsAsync();
    }
}