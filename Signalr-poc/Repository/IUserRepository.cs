using Signalr_poc.Entity;

namespace Signalr_poc.Repository;

public interface IUserRepository
{
    bool CreateUser(string username,string connectionId);
    bool Delete(string connectionId);
    User? GetUser(string connectionId);
    IEnumerable<User> GetUsers();
}

