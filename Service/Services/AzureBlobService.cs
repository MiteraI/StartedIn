using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Services.Interface;

namespace Service.Services;

public class AzureBlobService : IAzureBlobService
{
    private readonly IConfiguration _configuration;
    private readonly BlobContainerClient _avatarContainerClient;
    private readonly BlobContainerClient _postImagesContainerClient;
    private readonly string _azureBlobStorageKey;

    public AzureBlobService(IConfiguration configuration)
    {
        _configuration = configuration;
        _azureBlobStorageKey = configuration.GetValue<string>("AzureBlobStorageKey");

        BlobServiceClient blobServiceClient = new BlobServiceClient(_azureBlobStorageKey);

        _avatarContainerClient = blobServiceClient.GetBlobContainerClient("avatars");
        _postImagesContainerClient = blobServiceClient.GetBlobContainerClient("post-images");
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
    
    public async Task<string> UploadPostImage(IFormFile image)
    {
        // Create blob client from file name from IFormFile image with guid
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var blobClient = _postImagesContainerClient.GetBlobClient(fileName);

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

    public async Task<IList<string>> UploadPostImages(IList<IFormFile> image)
    {
        var imageUrls = new List<string>();
        if (image != null && image.Count > 0)
        {
            foreach (var img in image)
            {
                imageUrls.Add(await UploadPostImage(img));
            }
        }
        return imageUrls;
    }
}