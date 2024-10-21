namespace ApiContracts.PostDTO;

public class AddPostDto
{
    public int Id { get; set; }
    public int Userid { get; set; }
    public string Title { get; set; }
    public string PostsBody { get; set; }
}