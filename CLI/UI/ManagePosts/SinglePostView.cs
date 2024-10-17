using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly int postId;

    public SinglePostView(IPostRepository postRepository)
    {
        this._postRepository = postRepository;

    }

    public async Task showAsync()
    {
        Post post = await _postRepository.GetSinglePostAsync(postId);
        await SinglePostAsync();
    }

    private async Task SinglePostAsync()
    {
        //_postRepository.GetSinglePostAsync(postId);
    }
}