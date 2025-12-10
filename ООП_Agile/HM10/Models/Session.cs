namespace HM10.Models;

public class Session
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public int RoomId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string Notes { get; set; } = string.Empty;

    public Session()
    {
    }

    public Session(
        int id,
        int courseId,
        int teacherId,
        int groupId,
        int roomId,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        string notes = ""
    )
    {
        Id = id;
        CourseId = courseId;
        TeacherId = teacherId;
        GroupId = groupId;
        RoomId = roomId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Notes = notes;
    }

    public bool OverlapsWith(Session other)
    {
        if (Date.Date != other.Date.Date)
        {
            return false;
        }

        return StartTime < other.EndTime && EndTime > other.StartTime;
    }
}
