using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private const string FilePath = "post.json";

    public async Task<Post> AddPostAsync(Post post)
    {
        List<Post> posts = await LoadPostsAsync();
        post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        await SavePostsAsync(posts);
        return post;
    }

    public async Task UpdatePostAsync(Post postToUpdate)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? existingPost = posts.SingleOrDefault(p => p.Id == postToUpdate.Id);
        if (existingPost is null)
        {
            throw new NotFoundException($"Post with ID '{postToUpdate.Id}' not found");
        }

        posts.Remove(existingPost);
        posts.Add(postToUpdate);

        await SavePostsAsync(posts);
    }

    public async Task DeletePostAsync(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove is null)
        {
            throw new NotFoundException($"Post with ID '{id}' not found");
        }

        posts.Remove(postToRemove);
        await SavePostsAsync(posts);
    }

    public async Task<Post> GetSinglePostAsync(int id)
    {
        List<Post> posts = await LoadPostsAsync();
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null)
        {
            throw new NotFoundException($"Post with ID '{id}' not found");
        }

        return post;
    }

    public IQueryable<Post> GetManyPost()
    {
        return LoadPostsAsync().Result.AsQueryable();
    }

    private static async Task<List<Post>> LoadPostsAsync()
    {
        string json = await File.ReadAllTextAsync(FilePath);
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(json)!;
        return posts;
    }

    private static Task SavePostsAsync(List<Post> posts)
    {
        string json = JsonSerializer.Serialize(posts, new JsonSerializerOptions { WriteIndented = true });
        return File.WriteAllTextAsync(FilePath, json);
    }
}