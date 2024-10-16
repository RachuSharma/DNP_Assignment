using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> comments = new();

    public CommentInMemoryRepository()
    {
        _ = AddCommentAsync(new Comment(1, 1, "Best of luck for")).Result;
    }

    public Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateCommentAsync(Comment commentForUpdate)
    {
        Comment? commentToUpdate = comments.SingleOrDefault(c => c.Id == commentForUpdate.Id);
        if (commentToUpdate is null)
        {
            throw new Exception("Comment not found to update");
        }

        return Task.CompletedTask;
    }

    public Task DeleteCommentAsync(int id)
    {
        Comment? commentToDelete = comments.SingleOrDefault(c => c.Id == id);
        if (commentToDelete is null)
        {
            throw new Exception("There is no comment to delete");
        }

        comments.Remove(commentToDelete);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleCommentAsync(int id)
    {
        var comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment is null)
        {
            throw new Exception("Comment not Found");
        }

        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetAllComment()
    {
        return comments.AsQueryable();
    }
}