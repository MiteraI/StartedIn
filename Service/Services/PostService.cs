﻿using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

namespace Service.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostService> _logger;
        private readonly IAzureBlobService _azureBlobService;
        private readonly UserManager<User> _userManager;
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork, ILogger<PostService> logger, IAzureBlobService azureBlobService, UserManager<User> userManager) 
        {
            _postRepository = postRepository;
            _azureBlobService = azureBlobService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task CreateNewPost(string userId, Post post, List<IFormFile> picList)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                post.UserId = userId;
                post.CommentCount = 0;
                post.InteractionCount = 0;
                post.PostStatus = CrossCutting.Enum.Status.Pending;
                post.User = await _userManager.FindByIdAsync(userId);
                var result = _postRepository.Add(post);
                var imageURLs = await _azureBlobService.UploadPostImages(picList);
                post.PostImages = imageURLs.Select(url => new PostImage { ImageLink = url }).ToList();
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

        public async Task<IEnumerable<Post>> GetActivePostAsync(int pageIndex, int pageSize)
        {
            var postEntitiesList = await _postRepository.QueryHelper().Filter(c => c.PostStatus == CrossCutting.Enum.Status.Active)
                .Include(post => post.PostImages)
                .Include(post => post.User)
                .OrderBy(posts => posts.OrderByDescending(p => p.CreatedTime)).GetPagingAsync(pageIndex, pageSize);
            if (!postEntitiesList.Any())
            {
                throw new NotFoundException("Không có bài viết khả dụng !");
            }
            return postEntitiesList;
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

        public async Task<Post> GetPostsById(string postId)
        {
            var chosenPost = await _postRepository.QueryHelper()
                .Filter(post => post.Id.Equals(postId))
                .Include(post => post.PostImages)
                .Include(post => post.User)
                .GetOneAsync();
            if (chosenPost == null)
            {
                throw new NotFoundException("Không có bài viết khả dụng !");
            }
            return chosenPost;
        }
    }
}
