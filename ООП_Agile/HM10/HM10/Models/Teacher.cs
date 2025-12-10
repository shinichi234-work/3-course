namespace HM10.Models;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Teacher()
    {
    }

    public Teacher(int id, string name, string email = "")
    {
        Id = id;
        Name = name;
        Email = email;
    }
}