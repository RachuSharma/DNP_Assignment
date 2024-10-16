using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filepath = "user.json";

    public UserFileRepository()
    {
        if (!File.Exists(filepath))
        {
          File.WriteAllText(filepath, "[]");
        }
    }
    public async Task<User> AddUserAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 1;
        user.Id = maxId + 1;
        users.Add(user);
        
        
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new Exception("User Not Found to update");
        }

        users.Remove(existingUser);
        users.Add(user);

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
        return user;    }

    public async Task DeleteUserAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        var userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new("User not found");
        }

        users.Remove(userToRemove); 
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filepath, usersAsJson);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filepath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        var user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        return user;
    }

    public IQueryable<User> GetManyUser()
    {
        string userAsJson = File.ReadAllTextAsync(filepath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        return users.AsQueryable();
    }
}