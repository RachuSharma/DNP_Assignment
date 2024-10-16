using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    public Task<Post> AddPostAsync(Post post)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePostAsync(Post updatedPost)
    {
        throw new NotImplementedException();
    }

    public Task DeletePostAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Post> GetSinglePostAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Post> GetManyPost()
    {
        throw new NotImplementedException();
    }
}