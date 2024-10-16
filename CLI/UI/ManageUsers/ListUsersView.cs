using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public async Task showAsync()
    {
        Console.WriteLine();
        await ViewUserAsync();
    }

    private async Task ViewUserAsync()
    {
        IEnumerable<User> listUsersAsync = _userRepository.GetManyUser();
        List<User> users = listUsersAsync.OrderBy(u => u.Id).ToList();
        
        Console.WriteLine("List of Users :");
        Console.WriteLine("[");

        foreach (User user in users)
        {
            Console.WriteLine($"ID : {user.Id} - Username: {user.UserName}");
        }
        Console.WriteLine("]");
        Console.WriteLine();
    }
}