using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository
{
  /*  private readonly List<Post> posts = new();

    public PostInMemoryRepository()
    {
        _ = AddPostAsync (new Post(1, "MY FIRST Assignment", "I have startes my first assignment for c#")).Result;
    }
    
    public Task<Post> AddPostAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        
        posts.Add(post);
        return Task.FromResult(post);

    }

    public Task UpdatePostAsync(Post updatedPost)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == updatedPost.Id);
        if (updatedPost is null)
        {
            throw new Exception("Post not found to update");
        }
        posts.Remove(existingPost);
        posts.Add(updatedPost);

        return Task.CompletedTask;

    }

    public Task DeletePostAsync(int id)
    {
        var postToDelete = posts.SingleOrDefault(p => p.Id == id);
        if (posts is null)
        {
            throw new Exception("Post not found to delete");
        }

        posts.Remove(postToDelete);
        return Task.CompletedTask;
    }

    public Task<Post> GetSinglePostAsync(int id)
    {
        var post = posts.SingleOrDefault(p => p.Id == id);
        if (posts is null)
        {
            throw new Exception("Post not found");
        }

        return Task.FromResult(post);
    }

    public IQueryable<Post> GetManyPost()
    {
        return posts.AsQueryable();
    }
    */
}