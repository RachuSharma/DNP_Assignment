using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private const string FilePath = "user.json";

    public UserFileRepository()
    {
        if (!File.Exists(FilePath))
        {
          File.WriteAllText(FilePath, "[]");
        }
    }
    public async Task<User> AddUserAsync(User user)
    {
        List<User> users = await LoadUsers();
       user.Id = users.Count > 0 ? users.Max(u => u.Id) +1 : 1;
        users.Add(user);
        await SaveList(users);
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        List<User> users = await LoadUsers();
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new Exception("User Not Found to update");
        }

        users.Remove(existingUser);
        users.Add(user);

        await SaveList(users);
    }

    public async Task DeleteUserAsync(int id)
    {
        List<User> users = await LoadUsers();
        var userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new NotFoundException($"User not found with id: {id}");
        }

        users.Remove(userToRemove);
        await SaveList(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await LoadUsers();
        var user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        return user;
    }

    public IQueryable<User> GetManyUser()
       => LoadUsers().Result.AsQueryable();

    public async Task<User> GetSingleUserByUserNameAsync(string userName)
    {
        List<User> users = await LoadUsers();
        var user = users.SingleOrDefault(u => u.UserName == userName);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }
        return user;
    }

    private static Task SaveList(List<User> users)
    {
        string usersAsJson = ListToJson(users);
        return JsonToFileAsync(usersAsJson);
    }

    private static Task JsonToFileAsync(string json)
        => File.WriteAllTextAsync(FilePath, json);

    private static string ListToJson(List<User> list)
        => JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });

    private static async Task<List<User>> LoadUsers()
    {
        string usersAsJson = await ReadJsonAsync();
        return JsonToUserList(usersAsJson);
    }

    private static List<User> JsonToUserList(string usersAsJson)
        => JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

    private static Task<string> ReadJsonAsync()
        => File.ReadAllTextAsync(FilePath);
}