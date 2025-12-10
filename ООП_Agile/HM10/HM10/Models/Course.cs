namespace HM10.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public int Duration { get; set; }

    public Course()
    {
    }

    public Course(int id, string title, string code = "", int duration = 90)
    {
        Id = id;
        Title = title;
        Code = code;
        Duration = duration;
    }
}