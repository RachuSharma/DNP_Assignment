using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task StartAsync()
    {
        await StartMainMenu();

        Console.WriteLine("Existing App...*...*...");
    }

    /*
     To create a user
     1. While true
     2. take input from user
     3. Goto showAsync
     */
    public async Task StartMainMenu()
    {
        while (true)
        {
            Console.WriteLine("Please select what would you like to do");
            PrintMainMenu();
            string? selectOption = Console.ReadLine();
            switch (selectOption)
            {
                case "1":
                    ManageUserView manageUserView = new ManageUserView(userRepository);
                    await manageUserView.showAsync();
                    break;
                case "2":
                    ManagePostView managePostView = new ManagePostView(postRepository);
                    await managePostView.showAsync();
                    break;
            }
        }
    }

    private static void PrintMainMenu()
    {
        const string chooseOption = """
                                     1. Manage user          
                                     2. Manage Post 
                                    """;
        Console.WriteLine(chooseOption);
    }
}