using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Services.Interface;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Text;

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
    public async Task<string> UploadAvatarOrCover(IFormFile image)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var blobClient = _avatarContainerClient.GetBlobClient(fileName);

        using (var stream = image.OpenReadStream())
        using (var imageSharp = await Image.LoadAsync(stream))
        {
            // Resize the image to a smaller size (e.g., 300x300 pixels)
            imageSharp.Mutate(x => x.Resize(250, 250));

            // Compress and convert the image to JPEG format with 80% quality
            var encoder = new JpegEncoder { Quality = 80 };
            using (var memoryStream = new MemoryStream())
            {
                imageSharp.Save(memoryStream, encoder);
                memoryStream.Position = 0;

                // Upload the compressed image to Azure Blob Storage
                await blobClient.UploadAsync(memoryStream);
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
        using (var imageSharp = await Image.LoadAsync(stream))
        {
            // Resize the image to a smaller size (e.g., 1400x1400 pixels)
            imageSharp.Mutate(x => x.Resize(500, 500));

            // Compress and convert the image to JPEG format with 80% quality
            var encoder = new JpegEncoder { Quality = 80 };
            using (var memoryStream = new MemoryStream())
            {
                imageSharp.Save(memoryStream, encoder);
                memoryStream.Position = 0;

                // Upload the compressed image to Azure Blob Storage
                await blobClient.UploadAsync(memoryStream);
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