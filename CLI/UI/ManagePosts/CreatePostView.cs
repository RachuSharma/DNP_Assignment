using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
   

    public CreatePostView(IPostRepository postRepository, IUserRepository userRepository)
    {
        this._postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task ShowAsync()
    {
        Console.WriteLine();
        await CreatePostAsync();
    }
    
    private async Task CreatePostAsync()
    {
        Console.WriteLine("You are Creating Post");
        Console.WriteLine("Create TITLE: ");
        string? title = null;
        while (string.IsNullOrEmpty(title))
        {
            title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }
        }

        Console.WriteLine("Create Post body: ");
        string? postsBody = null;
        while (string.IsNullOrEmpty(postsBody))
        {
            postsBody = Console.ReadLine();
            if (string.IsNullOrEmpty(postsBody))
            {
                Console.WriteLine("Post cannot be empty.");
                return;
            }
        }
      
        Console.WriteLine("Please insert User ID: ");
        int userId;
        while (true)
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("ID cannot be empty.");
                continue;
            }
            if (int.TryParse(input, out userId))
            {
                bool userExists = await isUserExists(userId);
                if (userExists)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("User not found. Please enter a valid User ID.");
                }
            }
            else
            {
                Console.WriteLine("Could not parse the ID, please try again.");
            }
        }

        Console.WriteLine("You are creating a Post with the following information:");
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Post Body: {postsBody}");

        await AddPostAsync(userId, title, postsBody);
    }

    private async Task<bool> isUserExists(int userId)
    {
        var user = await _userRepository.GetSingleAsync(userId);
        return user != null;
    }

    private async Task AddPostAsync(int userId, string title, string postsBody)
    {
        Post post = new(userId, title, postsBody);
        Post added = await _postRepository.AddPostAsync(post);
        Console.WriteLine("Post created successfully, with Id: " + added.Id);
    }
}
