using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize);
        Task<IEnumerable<Post>> GetActivePostAsync(int pageIndex, int pageSize);
        Task CreateNewPost(string userId, Post post, List<IFormFile> picList);
        Task<Post> GetPostsById(string postId);

    }
}
