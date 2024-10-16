using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUserView
{
    private readonly IUserRepository _userRepository;

    public ManageUserView(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }

    public async Task showAsync()
    {
        Console.WriteLine();
        await ManageUserAsync();
    }

    public async Task ManageUserAsync()
    {
        while (true)
        {
           Console.WriteLine("Plwase select option ");
           Console.WriteLine("Press 3 for Create user :");
           Console.WriteLine("Press 4 for See list of User :");

           string? inputFromUser = Console.ReadLine();

           switch (inputFromUser)
           {
            case "3" :
                CreateUserView createUserView = new CreateUserView(_userRepository);
                await createUserView.ShowAsync();
                return;
            case "4" :
                ListUsersView listUsersView = new ListUsersView(_userRepository);
                await listUsersView.showAsync();
                return;
            
            default:
                Console.WriteLine("Invalid option, please try again.\n");
                break;
           }
           
        } 
    }
}