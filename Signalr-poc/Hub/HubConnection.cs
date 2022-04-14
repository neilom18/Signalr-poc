using Microsoft.AspNetCore.SignalR;
using Signalr_poc.DTOs;
using Signalr_poc.Repository;

namespace Signalr_poc;

public class HubConnection : Hub
{
    private readonly ILogger<HubConnection> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    public HubConnection(ILogger<HubConnection> logger, IUserRepository userRepository, IRoomRepository roomRepository)
    {
        _logger = logger;
        _userRepository ??= userRepository;
        _roomRepository ??= roomRepository;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation($"New connection in Hub {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _logger.LogInformation($"Connection with Hub finalized {0} | {1}", exception, Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task CreateUser(string username)
    {
        _userRepository.CreateUser(username, Context.ConnectionId);
        await Clients.Caller.SendAsync("UserCreated", username);
    }

    public async Task CreateRoom(string roomName)
    {
        if (_roomRepository.CreateRoom(roomName))
            await Clients.Caller.SendAsync("RoomCreated", roomName);
    }

    public async Task JoinRoom(string roomName)
    {
        var user = _userRepository.GetUser(Context.ConnectionId);
        var room = _roomRepository.GetRoom(roomName);
        room.AddUser(user);
        await Groups.AddToGroupAsync(user.ConnectionId, roomName);
        await Clients.Group(room.Name).SendAsync
            ("UserJoinedRoom", new GroupNotification {UserName = user.Name, RoomName = room.Name });
    }
}

