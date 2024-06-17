using Domain.Entities;

namespace Service.Services.Interface;

public interface IConnectionService
{ 
    Task CreateConnection(string senderId, string receiverId);

    Task RespondConnection(string connectionId, int response);

    Task<IEnumerable<Connection>> GetPendingConnections(int pageIndex, int pageSize, string receiverId);
}