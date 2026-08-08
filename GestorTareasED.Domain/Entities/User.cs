using GestorTareasED.Domain.Core;

namespace GestorTareasED.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Developer";
    public bool IsActive { get; set; } = true;

   
    public User()
    {
    }

    
    public User(string name, string email)
    {
        Name = name;
        Email = email;
        Role = "Developer";
        IsActive = true;
    }

    
    public User(string name, string email, string role)
    {
        Name = name;
        Email = email;
        Role = role;
        IsActive = true;
    }
}