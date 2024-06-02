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
    private readonly BlobContainerClient _postImagesContainerClient;
    private readonly string _azureBlobStorageAccountNameForAvatar;
    private readonly string _azureBlobStorageKeyForAvatar;
    private readonly string _azureBlobStorageAccountNameForPost;
    private readonly string _azureBlobStorageKeyForPost;

    public AzureBlobService(IConfiguration configuration)
    {
        _configuration = configuration;
        _azureBlobStorageAccountNameForAvatar = configuration.GetValue<string>("AzureBlobStorageAccountNameForAvatar");
        _azureBlobStorageKeyForAvatar = configuration.GetValue<string>("AzureBlobStorageKeyForAvatar");

        _azureBlobStorageAccountNameForPost = configuration.GetValue<string>("AzureBlobStorageAccountNameForPost");
        _azureBlobStorageKeyForPost = configuration.GetValue<string>("AzureBlobStorageKeyForPost");
        
        var azureCredentialsForAvatar = new StorageSharedKeyCredential(_azureBlobStorageAccountNameForAvatar, _azureBlobStorageKeyForAvatar);
        var azureCredentialForPost =
            new StorageSharedKeyCredential(_azureBlobStorageAccountNameForPost, _azureBlobStorageKeyForPost);
        
        var blobUriForAvatar = new Uri($"https://{_azureBlobStorageAccountNameForAvatar}.blob.core.windows.net");
        var blobServiceClientForAvatar = new BlobServiceClient(blobUriForAvatar, azureCredentialsForAvatar);

        var blobUriForPost = new Uri($"https://{_azureBlobStorageAccountNameForAvatar}.blob.core.windows.net");
        var blobServiceClientForPost = new BlobServiceClient(blobUriForPost, azureCredentialForPost);
        
        _avatarContainerClient = blobServiceClientForAvatar.GetBlobContainerClient("avatars");
        _postImagesContainerClient = blobServiceClientForPost.GetBlobContainerClient("postimages");
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