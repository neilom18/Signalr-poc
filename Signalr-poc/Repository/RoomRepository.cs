using Signalr_poc.Entity;

namespace Signalr_poc.Repository;

public class RoomRepository : IRoomRepository
{
    private readonly Dictionary<string, Room> _rooms;
    public RoomRepository()
    {
        _rooms ??= new Dictionary<string, Room>();
    }
    public bool CreateRoom(string roomName)
    {
        return _rooms.TryAdd(roomName, new Room(roomName));
    }

    public bool DeleteRoom(string roomName)
    {
        return _rooms.Remove(roomName);
    }

    public Room? GetRoom(string roomName)
    {
        _rooms.TryGetValue(roomName, out Room room);
        return room;
    }
}

