﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interface
{
    public interface ITeamRepository : IGenericRepository<Team, string>
    {
        Task<IEnumerable<Team>> GetTeamsByUserIdAsync(string userId);
        Task<IEnumerable<Team>> GetTeamByUserLeaderIdAsync(string userId);
        Task<IEnumerable<Team>> GetTeamByUserGuestIdAsync(string userId);
        Task<Team> GetTeamByIdAsync(string teamId);
    }
}
