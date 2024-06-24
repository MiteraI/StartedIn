using Domain.Entities;

namespace Service.Services.Interface;

public interface IConnectionService
{ 
    Task CreateConnection(string senderId, string receiverId);

    Task RespondConnection(string connectionId, int response);

    Task<IEnumerable<Connection>> GetPendingConnections(int pageIndex, int pageSize, string receiverId);
    Task<IEnumerable<Connection>> GetUserConnectionSendingRequest(int pageIndex, int pageSize, string senderId);
    Task<IEnumerable<Connection>> GetUserConnectionList(int pageIndex, int pageSize,string userId);
}