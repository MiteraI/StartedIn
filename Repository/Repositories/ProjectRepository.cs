using Domain.Context;
using Domain.Entities;
using Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ProjectRepository : GenericRepository<Project, string>, IProjectRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProjectRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsByTeamIdAsync(string teamId)
        {
            var projects = await _appDbContext.Projects.Where(p => p.TeamId == teamId)
                .OrderByDescending(p => p.CreatedTime)
                .ToListAsync();
            return projects;
        }
    }
}
