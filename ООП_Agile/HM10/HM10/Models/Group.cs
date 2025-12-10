namespace HM10.Models;
public class Group
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Size { get; set; }
    public int Year { get; set; }

    public Group()
    {
    }

    public Group(int id, string code, int size, int year = 0)
    {
        Id = id;
        Code = code;
        Size = size;
        Year = year;
    }
}