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
            var projects = await _appDbContext.Projects.Where(p => p.TeamId.Equals(teamId))
                .OrderByDescending(p => p.CreatedTime)
                .ToListAsync();
            return projects;
        }

        public async Task<Project> GetProjectById(string id)
        {
            var project = await _appDbContext.Projects.Where(p => p.Id.Equals(id))
                .Include(p => p.Team)
                .ThenInclude(t => t.TeamLeader)
                .Include(p => p.Phases)
                .ThenInclude(p => p.MajorTasks)
                .FirstOrDefaultAsync();
            return project;
        }
    }
}
