using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interface
{
    public interface IProjectRepository : IGenericRepository<Project,string>
    {
        Task<IEnumerable<Project>> GetProjectsByTeamIdAsync(string teamId);

        Task<Project> GetProjectById(string id);
    }
}
