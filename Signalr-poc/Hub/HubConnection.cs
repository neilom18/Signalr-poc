using MediatR;
using Microsoft.AspNetCore.SignalR;
using Signalr_poc.Commands.Rooms;
using Signalr_poc.Commands.Users.CreateUser;
using Signalr_poc.Commands.WebRTC.AddIceCandidate;
using Signalr_poc.Commands.WebRTC.CreateServerOffer;
using Signalr_poc.Commands.WebRTC.SetAnswer;
using Signalr_poc.DTOs;
using Signalr_poc.Feature.Rooms.Query.GetAllRooms;
using Signalr_poc.Repository;
using Signalr_poc.WebRTC;
using Signalr_pocRooms.Commands.Rooms;
using SIPSorcery.Net;

namespace Signalr_poc;

public class HubConnection : Hub
{
    private readonly ILogger<HubConnection> _logger;
    private readonly IMediator _mediator;
    public HubConnection(ILogger<HubConnection> logger,
                         IMediator mediator)
    {
        _logger = logger;
        _mediator ??= mediator;
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation($"New connection in Hub {Context.ConnectionId}");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _logger.LogInformation($"Connection with Hub finalized {exception} | {Context.ConnectionId}");
        return base.OnDisconnectedAsync(exception);
    }

    public async Task CreateUser(string username)
    {
        var result = await _mediator.Send(new CreateUserCommand() 
        {
            UserName = username, ConnectionId = Context.ConnectionId 
        });
        if (result)
        {
            await Clients.Caller.SendAsync("UserCreated", username);
        }
        return;
    }

    public async Task CreateRoom(string roomName)
    {
        var result = await _mediator.Send(new CreateRoomCommand() { RoomName = roomName });
        if (result)
        {
            await Clients.Caller.SendAsync("RoomCreated", roomName);
            return;
        }
    }

    public async Task<List<string>> GetAllRooms()
    {
        var rooms = await _mediator.Send(new GetAllRoomsQuery());
        return rooms.Rooms.Select(x => x.Name).ToList();
    }

    public async Task JoinRoom(string roomName)
    {
        var result = await _mediator.Send(new JoinRoomCommand() 
        {
            ConnectionId = Context.ConnectionId, RoomName = roomName
        });
        await Groups.AddToGroupAsync(result.User.ConnectionId, result.Room.Name);
        await Clients.Group(result.Room.Name).SendAsync
            ("UserJoinedRoom", new GroupNotification { UserName = result.User.Name, RoomName = result.Room.Name });

    }

    public async Task SetAnswer(string roomName, RTCSessionDescriptionInit sdp)
    {
        await _mediator.Send(new SetAnswerCommand() 
        {
            ConnectionId = Context.ConnectionId,RoomName = roomName, Sdp = sdp
        });
    }

    public async Task<RTCSessionDescriptionInit> CreateServerOffer(string roomName)
    {
        var result = await _mediator.Send(new CreateServerOfferCommand() 
        {
            RoomName = roomName, ConnectionId = Context.ConnectionId
        });
        return result.Sdp;
    }

    public void AddIceCandidate(string candidate, string roomName)
    {
        var iceCandidate = new RTCIceCandidateInit { candidate = candidate };
        _mediator.Send(new AddIceCandidateCommand() 
        {
            IceInfo = new IceInfoDTO() { IceCandidateInit = iceCandidate, RoomName = roomName },
            ConnectionId = Context.ConnectionId 
        }).GetAwaiter();
    }
}

