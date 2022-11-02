namespace Model.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserResponse()
    {
    }

    public UserResponse(Guid id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }
}
