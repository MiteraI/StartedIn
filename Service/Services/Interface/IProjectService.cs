using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface IProjectService
{
    Task CreateNewProject(NewProjectCreateDTO projectCreateDto);
    Task<IEnumerable<Project>> GetProjectsByTeamId(string teamId);
    Task<Project> GetProjectById(string id);
}