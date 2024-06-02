using Microsoft.AspNetCore.Http;

namespace Service.Services.Interface;

public interface IAzureBlobService
{
    Task<string> UploadAvatar(IFormFile image);
    Task<string> UploadPostImage(IFormFile image);
    Task<IList<string>> UploadPostImages(IList<IFormFile> image);
}