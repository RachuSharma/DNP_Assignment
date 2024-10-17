using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment updatedComment);
    Task DeleteCommentAsync(int id);
    Task<Comment> GetSingleCommentAsync(int id);
    IQueryable<Comment> GetAllComment();
}