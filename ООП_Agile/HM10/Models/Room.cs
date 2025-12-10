namespace HM10.Models;

public class Room
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Building { get; set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; set; } = new();

    public Room()
    {
    }

    public Room(
        int id, 
        string code, 
        int capacity, 
        string building = ""
    )
    {
        Id = id;
        Code = code;
        Capacity = capacity;
        Building = building;
    }
}
