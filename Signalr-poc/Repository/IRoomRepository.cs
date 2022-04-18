using Signalr_poc.Entity;

namespace Signalr_poc.Repository;

public interface IRoomRepository
{
    bool CreateRoom(string roomName);
    bool DeleteRoom(string roomName);
    Room? GetRoom(string roomName);
    IEnumerable<Room> GetAllRooms();
}

