namespace Entities;

public class Post
{
    public int Id { get; set; }
    public int Userid { get; set; }
    public string Title { get; set; }
    public string PostsBody { get; set; }

    public Post(int userid,string title, string postsbody)
    {
        Userid = userid;
        PostsBody = postsbody;
        Title = title;
    }

}