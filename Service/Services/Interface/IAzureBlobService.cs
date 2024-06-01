using Microsoft.AspNetCore.Http;

namespace Service.Services.Interface;

public interface IAzureBlobService
{
    Task<string> UploadAvatar(IFormFile image);
}