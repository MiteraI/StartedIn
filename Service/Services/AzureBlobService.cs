using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Services.Interface;

namespace Service.Services;

public class AzureBlobService : IAzureBlobService
{
    private readonly IConfiguration _configuration;
    private readonly BlobContainerClient _avatarContainerClient;
    private readonly string _azureBlobStorageAccountName;
    private readonly string _azureBlobStorageKey;

    public AzureBlobService(IConfiguration configuration)
    {
        _configuration = configuration;
        _azureBlobStorageAccountName = configuration.GetValue<string>("AzureBlobStorageAccountName");
        _azureBlobStorageKey = configuration.GetValue<string>("AzureBlobStorageKey");
        var azureCredentials = new StorageSharedKeyCredential(_azureBlobStorageAccountName, _azureBlobStorageKey);

        var blobUri = new Uri($"https://{_azureBlobStorageAccountName}.blob.core.windows.net");
        var blobServiceClient = new BlobServiceClient(blobUri, azureCredentials);
    }
    public async Task<string> UploadAvatar(IFormFile image)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var blobClient = _avatarContainerClient.GetBlobClient(fileName);

        using (var stream = image.OpenReadStream())
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();
                await blobClient.UploadAsync(new MemoryStream(imageBytes));
            }
        }
        return blobClient.Uri.AbsoluteUri;
    }
}