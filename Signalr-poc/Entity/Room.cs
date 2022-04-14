namespace Signalr_poc.Entity;

public class Room
{
    public string Name { get;private set; }
    public List<User> Users { get; set; }


    public Room(string name)
    {
        Name = name;
        Users = new List<User>();
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void RemoveUser(User user)
    {
        Users.Remove(user);
    }
}

