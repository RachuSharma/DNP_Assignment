using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository 
{
   /* private readonly List<User> users = new();

    public UserInMemoryRepository()
    {
        _ = AddUserAsync(new User("Rachana", "1234")).Result;
    }

    public Task<User> AddUserAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task<User> UpdateUserAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new Exception("User Not Found to update");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int id)
    {
        var userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null)
        {
            throw new("User not found");
        }

        users.Remove(userToRemove);

        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        var user = users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        return Task.FromResult(user);
    }

    public IQueryable<User> GetManyUser()
    {
        return users.AsQueryable();
    }*/
}