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
        if (!IsValidImageFile(image))
        {
            throw new ArgumentException("The uploaded file is not a valid image.");
        }
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var fileExtension = Path.GetExtension(image.FileName).ToLower();
        var validExtensions = new[] { ".png", ".jpg", ".jpeg" };
        if (!validExtensions.Contains(fileExtension))
        {
            throw new InvalidOperationException("Unsupported file format. Please upload a .png, .jpg, or .jpeg file.");
        }
        var blobClient = _avatarContainerClient.GetBlobClient(fileName);

        using (var stream = image.OpenReadStream())
        using (var imageSharp = await Image.LoadAsync(stream))
        {
            imageSharp.Mutate(x => x.Resize(300, 300));
            var encoder = new JpegEncoder { Quality = 80 };
            using (var memoryStream = new MemoryStream())
            {
                imageSharp.Save(memoryStream, encoder);
                memoryStream.Position = 0;
                await blobClient.UploadAsync(memoryStream);
            }
        }

        return blobClient.Uri.AbsoluteUri;
    }
    
    public async Task<string> UploadPostImage(IFormFile image)
    {
        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var fileExtension = Path.GetExtension(image.FileName).ToLower();
        var validExtensions = new[] { ".png", ".jpg", ".jpeg" };
        if (!validExtensions.Contains(fileExtension))
        {
            throw new InvalidOperationException("Unsupported file format. Please upload a .png, .jpg, or .jpeg file.");
        }
        var blobClient = _postImagesContainerClient.GetBlobClient(fileName);

        using (var stream = image.OpenReadStream())
        using (var imageSharp = await Image.LoadAsync(stream))
        {
            imageSharp.Mutate(x => x.Resize(1400, 1400));
            var encoder = new JpegEncoder { Quality = 80 };
            using (var memoryStream = new MemoryStream())
            {
                imageSharp.Save(memoryStream, encoder);
                memoryStream.Position = 0;
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