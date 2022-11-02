namespace Model;

public class User : EntityBase
{
    public string Email { get; set; }
    public string Password { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

    public User()
    {
    }

    public User(Guid id, string email, string password, ICollection<Order> orders)
    {
        Id = id;
        Email = email;
        Password = password;
        Orders = orders;
    }
}
