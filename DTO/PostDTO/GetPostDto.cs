namespace ApiContracts.PostDTO;

public class GetPostDto
{
    public int Id { get; set; }
    public int Userid { get; set; }
    public string Title { get; set; }
    public string PostsBody { get; set; }
}