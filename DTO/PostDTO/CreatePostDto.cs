namespace ApiContracts.PostDTO;

public class CreatePostDto
{
    public int Userid { get; set; }
    public string Title { get; set; }
    public string PostsBody { get; set; }
}