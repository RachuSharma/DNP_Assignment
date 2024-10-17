using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostView
{
    private readonly IPostRepository _postRepository;

    public ListPostView(IPostRepository postRepository)
    {
        this._postRepository = postRepository;
    }
    public async Task showAsync()
    {
        Console.WriteLine();
        await ListPostAsync();
    }

    private async Task ListPostAsync()
    {
        IEnumerable<Post> listPostAsync = _postRepository.GetManyPost();
        List<Post> posts = listPostAsync.OrderBy(u => u.Id).ToList();
        
        Console.WriteLine("List of Posts :");
        Console.WriteLine("[");

        foreach (Post post in posts)
        {
            Console.WriteLine($"Post Id : {post.Id} - Title : {post.Title}  \n PostBody : {post.PostsBody} ");
        }
        Console.WriteLine("]");
        Console.WriteLine();
    }
}