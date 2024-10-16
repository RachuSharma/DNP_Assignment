using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
   

    public CreatePostView(IPostRepository postRepository)
    {
        this._postRepository = postRepository;
    }

    public async Task showAsync()
    {
        Console.WriteLine();
        await CreatePostAsync();
    }
    
    //TODO 

    private async Task CreatePostAsync()
    {
        Console.WriteLine("You are Creating Post");
        Console.WriteLine("Create TITLE : ");
        String? title = null;
        while (string.IsNullOrEmpty(title))
        {
            title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty. ");
                return;
            }

            Console.WriteLine("Create Post body");
            String? postsBody = null;
            while (string.IsNullOrEmpty(postsBody))
            {
                postsBody = Console.ReadLine();
                if (string.IsNullOrEmpty(postsBody))
                {
                    Console.WriteLine("Post cannot be empty. ");
                    return;
                }

                int userId = InsertUserId();
                
                Console.WriteLine("You are creating a Post with following Information");
                Console.WriteLine($"Title : {title}");
                Console.WriteLine($"PostBody: {postsBody}");

                await AddPostAsync(userId, title, postsBody);
            }
        }
    }

    private int InsertUserId()
    {
        int userId;
        // take input from user 
        //make int
        // check if there is a user with that id in database else call insert id again 
        Console.WriteLine("Please insert ID :  ");
        while (true)
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("ID cannot be empty ");
                return InsertUserId();
            }

            if (int.TryParse(input, out userId))
            {
                var existUser = isUserExiest(userId);
                if (existUser.Result) ;
                {
                    
                }
            }
        }
    }

    private async Task<bool> isUserExiest(int userId)
    {
        var user = await _userRepository.GetSingleAsync(userId);
        return user != null;
    }

    private async Task AddPostAsync( int userId, string title, string postsBody)
    {
        Post post = new(userId, title, postsBody);
        Post added = await _postRepository.AddPostAsync(post);
        Console.WriteLine("Post created successfully, with Id: " + added.Id);
    }
}