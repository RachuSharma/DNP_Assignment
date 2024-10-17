using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private const string FilePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(FilePath))
        {
            File.WriteAllText(FilePath, "[]");
        }
    }

    public async Task AddCommentAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();

        string commentsAsJson = await File.ReadAllTextAsync(FilePath);
        comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;
        comment.Id = maxId + 1;
        comments.Add(comment);
    }


    public async Task UpdateCommentAsync(Comment updateThisComment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment exiestComment = await GetSingleCommentAsync(updateThisComment.Id);

        comments.Remove(exiestComment);
        comments.Add(updateThisComment);

        await SaveList(comments);
    }

    public async Task DeleteCommentAsync(int id)
    {
        
        List<Comment> comments = await LoadCommentsAsync();
        
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null)
        {
            throw new NotFoundException($"Comment with ID '{id}' not found");
        }

        comments.Remove(commentToRemove);

        await SaveList(comments);


    }
    

    public async Task<Comment> GetSingleCommentAsync(int id)
    {
        List<Comment> comments = await LoadCommentsAsync();

        var comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment is null)
        {
            throw new Exception("Comment not Found");
        }

        return comment;
    }

    public IQueryable<Comment> GetAllComment()
        => LoadCommentsAsync().Result.AsQueryable();

    private static Task SaveList(List<Comment> comments)
    {
        string commentsAsJson = ListToJson(comments);
        return JsonToFileAsync(commentsAsJson);
    }

    private static Task JsonToFileAsync(string json)
        => File.WriteAllTextAsync(FilePath, json);

    private static string ListToJson(List<Comment> list)
        => JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });

    private async Task<List<Comment>> LoadCommentsAsync()
    {
        string commentsAsJson = await ReadJsonAsync();
        return JsonToCommentList(commentsAsJson);
    }


    private static List<Comment> JsonToCommentList(string commentsAsJson)
        => JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

    private static Task<string> ReadJsonAsync()
        => File.ReadAllTextAsync(FilePath);

}