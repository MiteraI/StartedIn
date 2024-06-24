using CrossCutting.Enum;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

namespace Service.Services;

public class ConnectionService : IConnectionService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConnectionRepository _connectionRepository;
    private readonly ILogger<ConnectionService> _logger;

    public ConnectionService(UserManager<User> userManager, IUnitOfWork unitOfWork,
        IConnectionRepository connectionRepository)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _connectionRepository = connectionRepository;
    }
    
    public async Task CreateConnection(string senderId, string receiverId)
    {
        var receiver = await _userManager.FindByIdAsync(receiverId);
        var sender = await _userManager.FindByIdAsync(senderId);
        if (receiver == null || sender == null)
        {
            throw new NotFoundException("Không tìm thấy người dùng");
        }

        try
        {
            _unitOfWork.BeginTransaction();
            var connection = new Connection
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                ConnectionStatus = ConnectionStatus.Pending
            };
            _connectionRepository.Add(connection);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating connection");
        }
        
    }

    public async Task RespondConnection(string connectionId, int response)
    {
        // if response = 0, means reject
        // if response = 1, means accept
        var connection = await GetConnectionById(connectionId);
        if (response == 0)
        {
            connection.ConnectionStatus = ConnectionStatus.Rejected;
        }

        if (response == 1)
        {
            connection.ConnectionStatus = ConnectionStatus.Accepted;
        }

        _connectionRepository.SaveChangesAsync();

    }

    public async Task<Connection> GetConnectionById(string connectionId)
    {
        var connection = await _connectionRepository.GetOneAsync(connectionId);
        return connection;
    }

    public async Task<IEnumerable<Connection>> GetPendingConnections(int pageIndex, int pageSize, string receiverId)
    {
        var connections = await _connectionRepository.QueryHelper()
            .Filter(c => c.ConnectionStatus == ConnectionStatus.Pending && c.ReceiverId == receiverId)
            .Include(c => c.Sender)
            .OrderBy(c => c.OrderByDescending(c => c.CreatedTime))
            .GetPagingAsync(pageIndex, pageSize);
        if (!connections.Any())
        {
            throw new NotFoundException("Không tìm thấy lời mời kết nối");
        }
        return connections;
    }
    public async Task<IEnumerable<Connection>> GetUserConnectionSendingRequest(int pageIndex, int pageSize, string senderId)
    {
        var connections = await _connectionRepository.QueryHelper()
            .Filter(c => c.ConnectionStatus == ConnectionStatus.Pending && c.SenderId == senderId)
            .Include(c => c.Receiver)
            .OrderBy(c => c.OrderByDescending(c => c.CreatedTime))
            .GetPagingAsync(pageIndex, pageSize);
        if (!connections.Any())
        {
            throw new NotFoundException("Không tìm thấy lời mời kết nối");
        }
        return connections;
    }
    public async Task<IEnumerable<Connection>> GetUserConnectionList(int pageIndex, int pageSize, string userId)
    {
        var connections = await _connectionRepository.QueryHelper()
            .Filter(c => (c.SenderId == userId || c.ReceiverId == userId) && c.ConnectionStatus == ConnectionStatus.Accepted)
            .Include(c => c.Receiver)
            .Include(c => c.Sender)
            .GetPagingAsync(pageIndex, pageSize);
        if (!connections.Any())
        {
            throw new NotFoundException("Không tìm thấy lời mời kết nối");
        }
        return connections;
    }
}