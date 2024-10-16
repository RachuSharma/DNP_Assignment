using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        await CreateUserAsync();
        return;
    }

    private async Task CreateUserAsync()
    {
        while (true)
        {
            Console.WriteLine("You are Creating User");
            Console.WriteLine("Please create user name: ");
            String? name = null;
            while (string.IsNullOrEmpty(name))
            {
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Name cannot be empty. ");
                }
            }

            Console.WriteLine("Please create password: ");
            string? password = null;
            while (string.IsNullOrEmpty(password))
            {
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Password cannot be empty.");
                }
                break;
            }

            Console.WriteLine("You Created a user with following Username and Password");
            Console.WriteLine($"User name: {name}");
            Console.WriteLine($"Password: {password}");
            
            await AddUserAsync(name, password);
            break;
        }
    }

    private async Task<User> AddUserAsync(string name, string password)
    {
        User user = new User(name, password);
        return await _userRepository.AddUserAsync(user);
    }
}