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
    
    public async Task<Connection> CreateConnection(string senderId, string receiverId)
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
            return connection;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating connection");
            throw new Exception("Gửi lời mời thất bại");
        }
        
    }

    public async Task<Connection> AcceptConnection(string connectionId)
    {
        var connection = await GetConnectionById(connectionId);
        if (connection is null)
        {
            throw new NotFoundException("Không tìm thấy kết nối");
        }
        connection.ConnectionStatus = ConnectionStatus.Accepted;
        await _connectionRepository.SaveChangesAsync();
        return connection;
    }

    public async Task<Connection> GetConnectionById(string connectionId)
    {
        var connection = await _connectionRepository.QueryHelper()
            .Filter(c => c.Id.Equals(connectionId))
            .Include(c => c.Receiver)
            .Include(c => c.Sender)
            .GetOneAsync();
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

    public async Task CancelConnection(string connectionId)
    {
        try {
            _unitOfWork.BeginTransaction();
            var chosenConnection = await _connectionRepository.GetOneAsync(connectionId);
            if (chosenConnection == null)
            {
                throw new NotFoundException("Không tìm thấy kết nối người dùng");
            }
            await _connectionRepository.DeleteByIdAsync(chosenConnection.Id);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Lỗi khi xoá kết nối.");
        }
       
    }
}