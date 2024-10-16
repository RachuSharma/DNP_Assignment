namespace Entities;

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}