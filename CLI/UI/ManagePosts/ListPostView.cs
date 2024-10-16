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
        throw new NotImplementedException();
    }
}