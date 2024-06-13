using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface ITeamService
    {
        Task CreateNewTeam(string userId, Team team, Project project); 
    }
}
