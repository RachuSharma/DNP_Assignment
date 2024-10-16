namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string CommentsBody { get; set; }

    public Comment(int postId, int userId, string commentsBody)
    {
        PostId = postId;
        UserId = userId;
       CommentsBody = commentsBody;
    }
}