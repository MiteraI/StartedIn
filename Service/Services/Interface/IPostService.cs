using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize);
        Task CreateNewPost(string userId, Post post);

    }
}
