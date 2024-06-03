using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;

namespace Repository.Repositories
{
    public class PostRepository : GenericRepository<Post, string>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }
    }
}
