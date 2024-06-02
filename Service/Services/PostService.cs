using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostService> _logger;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, ILogger<PostService> logger) 
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task CreateNewPost(string userId, Post post)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                post.UserId = userId;
                post.CommentCount = 0;
                post.InteractionCount = 0;
                var result = _postRepository.Add(post);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating post");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(int pageIndex, int pageSize)
        {
            var postEntitiesList = await _postRepository.QueryHelper()
                .Include(post => post.PostImages)
                .Include(post => post.User)
                .OrderBy(posts => posts.OrderByDescending(p=>p.CreatedTime)).GetPagingAsync(pageIndex, pageSize);
            if (!postEntitiesList.Any()) 
            {
                throw new NotFoundException("Không có bài viết khả dụng !");
            }
            return postEntitiesList;
        }
    }
}
