using Signalr_poc.Entity;

namespace Signalr_poc.Repository;

public class UserRepository : IUserRepository
{

    private readonly Dictionary<string, User> _users;
    public UserRepository()
    {
        _users ??= new Dictionary<string, User>();
    }
    public bool CreateUser(string username, string connectionId)
    {
        return _users.TryAdd(connectionId, new User(username, connectionId));
    }

    public bool Delete(string connectionId)
    {
        return _users.Remove(connectionId);
    }

    public User? GetUser(string connectionId)
    {
        _users.TryGetValue(connectionId, out var user);
        return user;
    }

    public IEnumerable<User> GetUsers()
    {
        return _users.Values;
    }
}

