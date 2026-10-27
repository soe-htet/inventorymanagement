namespace InventoryManagement.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }

    public User(
        string userName,
        string passwordHash,
        string role = "User")
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username is required.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash is required.");

        if (role != "Admin" && role != "User")
            throw new ArgumentException("Invalid user role.");

        UserName = userName;
        PasswordHash = passwordHash;
        Role = role;
    }
}