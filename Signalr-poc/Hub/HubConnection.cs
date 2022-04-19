using Microsoft.AspNetCore.SignalR;
using Signalr_poc.DTOs;
using Signalr_poc.Entity;
using Signalr_poc.Repository;
using Signalr_poc.WebRTC;
using SIPSorcery.Net;

namespace Signalr_poc;

public class HubConnection : Hub
{
    private readonly ILogger<HubConnection> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IPeerConnectionManager _peerConnectionManager;
    public HubConnection(ILogger<HubConnection> logger, IUserRepository userRepository, IRoomRepository roomRepository, IPeerConnectionManager peerConnectionManager)
    {
        _logger = logger;
        _userRepository ??= userRepository;
        _roomRepository ??= roomRepository;
        _peerConnectionManager ??= peerConnectionManager;
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

    public async Task<List<string>> GetAllRooms()
    {
        return _roomRepository.GetAllRooms().Select(x => x.Name).ToList();
    }

    public async Task JoinRoom(string roomName)
    {
        var user = _userRepository.GetUser(Context.ConnectionId);
        var room = _roomRepository.GetRoom(roomName);
        if (!user.Rooms.Contains(room))
        {
            var peerConnection = _peerConnectionManager.CreatePeer();
            room.AddPeerConnection(Context.ConnectionId, peerConnection);
            user.Rooms.Add(room);
            await Groups.AddToGroupAsync(user.ConnectionId, room.Name);
            await Clients.Group(room.Name).SendAsync
                ("UserJoinedRoom", new GroupNotification { UserName = user.Name, RoomName = room.Name });
        }
    }

    public async Task SetAnswer(string roomName, RTCSessionDescriptionInit sdp)
    {
        var room = _roomRepository.GetRoom(roomName);
        if (room is null) return;
        var peerConnection = room.GetPeerConection(Context.ConnectionId);
        if (peerConnection is null) return;
        _peerConnectionManager.SetRemoteDescription(sdp, peerConnection);
    }

    public async Task<RTCSessionDescriptionInit> GetServerOffer(string roomName)
    {
        var room = _roomRepository.GetRoom(roomName);
        var peerConnection = room.GetPeerConection(Context.ConnectionId);
        return _peerConnectionManager.CreateOffer(peerConnection, room, Context.ConnectionId);
    }

    public void AddIceCandidate(string candidate, string roomName)
    {
        var iceCandidate = new RTCIceCandidateInit { candidate = candidate };
        var iceInfoDTO = new IceInfoDTO() { iceCandidateInit = iceCandidate, roomName = roomName };
        _peerConnectionManager.AddIceCandidate(iceInfoDTO, Context.ConnectionId);
    }
}

