namespace HM10.Storage;

using System.Text.Json;
using HM10.Models;
using HM10.Validators;
public class Database
{
    private const string DefaultPath = "schedule_db.json";

    private List<Room> _rooms = new();
    private List<Teacher> _teachers = new();
    private List<Group> _groups = new();
    private List<Course> _courses = new();
    private List<Session> _sessions = new();

    private int _nextRoomId = 1;
    private int _nextTeacherId = 1;
    private int _nextGroupId = 1;
    private int _nextCourseId = 1;
    private int _nextSessionId = 1;

    public IReadOnlyList<Room> Rooms => _rooms;
    public IReadOnlyList<Teacher> Teachers => _teachers;
    public IReadOnlyList<Group> Groups => _groups;
    public IReadOnlyList<Course> Courses => _courses;
    public IReadOnlyList<Session> Sessions => _sessions;

    public void Initialize(string path = DefaultPath)
    {
        if (File.Exists(path))
        {
            throw new InvalidOperationException(
                "База данных уже существует"
            );
        }

        Save(path);
        Console.WriteLine($"База данных создана: {path}");
    }

    public void Save(string path = DefaultPath)
    {
        var data = new DatabaseSnapshot
        {
            Rooms = _rooms,
            Teachers = _teachers,
            Groups = _groups,
            Courses = _courses,
            Sessions = _sessions,
            NextRoomId = _nextRoomId,
            NextTeacherId = _nextTeacherId,
            NextGroupId = _nextGroupId,
            NextCourseId = _nextCourseId,
            NextSessionId = _nextSessionId
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }

    public void Load(string path = DefaultPath)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                $"Файл базы данных не найден: {path}"
            );
        }

        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<DatabaseSnapshot>(json);

        if (data == null)
        {
            throw new InvalidOperationException(
                "Не удалось загрузить базу данных"
            );
        }

        _rooms = data.Rooms;
        _teachers = data.Teachers;
        _groups = data.Groups;
        _courses = data.Courses;
        _sessions = data.Sessions;
        _nextRoomId = data.NextRoomId;
        _nextTeacherId = data.NextTeacherId;
        _nextGroupId = data.NextGroupId;
        _nextCourseId = data.NextCourseId;
        _nextSessionId = data.NextSessionId;
    }

    public Room AddRoom(string code, int capacity, string building = "")
    {
        var room = new Room(_nextRoomId, code, capacity, building);
        Validators.RoomValidator.Validate(room);

        if (_rooms.Any(r => r.Code == code))
        {
            throw new ArgumentException(
                $"Аудитория с кодом {code} уже существует"
            );
        }

        _rooms.Add(room);
        _nextRoomId++;
        return room;
    }

    public Room? GetRoom(int id)
    {
        return _rooms.FirstOrDefault(r => r.Id == id);
    }

    public Room? GetRoomByCode(string code)
    {
        return _rooms.FirstOrDefault(r => r.Code == code);
    }

    public bool UpdateRoom(
        int id, 
        string? code = null, 
        int? capacity = null, 
        string? building = null
    )
    {
        var room = GetRoom(id);
        if (room == null)
        {
            return false;
        }

        if (code != null)
        {
            room.Code = code;
        }

        if (capacity.HasValue)
        {
            room.Capacity = capacity.Value;
        }

        if (building != null)
        {
            room.Building = building;
        }

        Validators.RoomValidator.Validate(room);
        return true;
    }

    public bool DeleteRoom(int id)
    {
        var room = GetRoom(id);
        if (room == null)
        {
            return false;
        }

        if (_sessions.Any(s => s.RoomId == id))
        {
            throw new InvalidOperationException(
                "Нельзя удалить аудиторию, у которой есть занятия"
            );
        }

        _rooms.Remove(room);
        return true;
    }

    public Teacher AddTeacher(string name, string email = "")
    {
        var teacher = new Teacher(_nextTeacherId, name, email);
        Validators.TeacherValidator.Validate(teacher);

        _teachers.Add(teacher);
        _nextTeacherId++;
        return teacher;
    }

    public Teacher? GetTeacher(int id)
    {
        return _teachers.FirstOrDefault(t => t.Id == id);
    }

    public bool UpdateTeacher(int id, string? name = null, string? email = null)
    {
        var teacher = GetTeacher(id);
        if (teacher == null)
        {
            return false;
        }

        if (name != null)
        {
            teacher.Name = name;
        }

        if (email != null)
        {
            teacher.Email = email;
        }

        Validators.TeacherValidator.Validate(teacher);
        return true;
    }

    public bool DeleteTeacher(int id)
    {
        var teacher = GetTeacher(id);
        if (teacher == null)
        {
            return false;
        }

        if (_sessions.Any(s => s.TeacherId == id))
        {
            throw new InvalidOperationException(
                "Нельзя удалить преподавателя, у которого есть занятия"
            );
        }

        _teachers.Remove(teacher);
        return true;
    }

    public Group AddGroup(string code, int size, int year = 0)
    {
        var group = new Group(_nextGroupId, code, size, year);
        Validators.GroupValidator.Validate(group);

        if (_groups.Any(g => g.Code == code))
        {
            throw new ArgumentException(
                $"Группа с кодом {code} уже существует"
            );
        }

        _groups.Add(group);
        _nextGroupId++;
        return group;
    }

    public Group? GetGroup(int id)
    {
        return _groups.FirstOrDefault(g => g.Id == id);
    }

    public Group? GetGroupByCode(string code)
    {
        return _groups.FirstOrDefault(g => g.Code == code);
    }

    public bool UpdateGroup(
        int id, 
        string? code = null, 
        int? size = null, 
        int? year = null
    )
    {
        var group = GetGroup(id);
        if (group == null)
        {
            return false;
        }

        if (code != null)
        {
            group.Code = code;
        }

        if (size.HasValue)
        {
            group.Size = size.Value;
        }

        if (year.HasValue)
        {
            group.Year = year.Value;
        }

        Validators.GroupValidator.Validate(group);
        return true;
    }

    public bool DeleteGroup(int id)
    {
        var group = GetGroup(id);
        if (group == null)
        {
            return false;
        }

        if (_sessions.Any(s => s.GroupId == id))
        {
            throw new InvalidOperationException(
                "Нельзя удалить группу, у которой есть занятия"
            );
        }

        _groups.Remove(group);
        return true;
    }

    public Course AddCourse(string title, string code = "", int duration = 90)
    {
        var course = new Course(_nextCourseId, title, code, duration);
        Validators.CourseValidator.Validate(course);

        _courses.Add(course);
        _nextCourseId++;
        return course;
    }

    public Course? GetCourse(int id)
    {
        return _courses.FirstOrDefault(c => c.Id == id);
    }

    public bool UpdateCourse(
        int id, 
        string? title = null, 
        string? code = null, 
        int? duration = null
    )
    {
        var course = GetCourse(id);
        if (course == null)
        {
            return false;
        }

        if (title != null)
        {
            course.Title = title;
        }

        if (code != null)
        {
            course.Code = code;
        }

        if (duration.HasValue)
        {
            course.Duration = duration.Value;
        }

        Validators.CourseValidator.Validate(course);
        return true;
    }

    public bool DeleteCourse(int id)
    {
        var course = GetCourse(id);
        if (course == null)
        {
            return false;
        }

        if (_sessions.Any(s => s.CourseId == id))
        {
            throw new InvalidOperationException(
                "Нельзя удалить курс, у которого есть занятия"
            );
        }

        _courses.Remove(course);
        return true;
    }

    public Session AddSession(
        int courseId,
        int teacherId,
        int groupId,
        int roomId,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        string notes = "",
        bool force = false
    )
    {
        var session = new Session(
            _nextSessionId,
            courseId,
            teacherId,
            groupId,
            roomId,
            date,
            startTime,
            endTime,
            notes
        );

        Validators.SessionValidator.Validate(session);

        if (GetCourse(courseId) == null)
        {
            throw new ArgumentException($"Курс с ID {courseId} не найден");
        }

        if (GetTeacher(teacherId) == null)
        {
            throw new ArgumentException(
                $"Преподаватель с ID {teacherId} не найден"
            );
        }

        if (GetGroup(groupId) == null)
        {
            throw new ArgumentException($"Группа с ID {groupId} не найдена");
        }

        if (GetRoom(roomId) == null)
        {
            throw new ArgumentException(
                $"Аудитория с ID {roomId} не найдена"
            );
        }

        if (!force)
        {
            var conflicts = FindConflicts(session);
            if (conflicts.Count > 0)
            {
                var conflict = conflicts[0];
                throw new InvalidOperationException($"Конфликт: {conflict}");
            }
        }

        _sessions.Add(session);
        _nextSessionId++;
        return session;
    }

    public List<string> FindConflicts(Session newSession)
    {
        var conflicts = new List<string>();

        foreach (var existing in _sessions)
        {
            if (existing.Id == newSession.Id)
            {
                continue;
            }

            if (!newSession.OverlapsWith(existing))
            {
                continue;
            }

            if (existing.RoomId == newSession.RoomId)
            {
                var room = GetRoom(existing.RoomId);
                conflicts.Add(
                    $"Аудитория {room?.Code} занята " +
                    $"{existing.StartTime}-{existing.EndTime} " +
                    $"(занятие ID={existing.Id})"
                );
            }

            if (existing.TeacherId == newSession.TeacherId)
            {
                var teacher = GetTeacher(existing.TeacherId);
                conflicts.Add(
                    $"Преподаватель {teacher?.Name} занят " +
                    $"{existing.StartTime}-{existing.EndTime} " +
                    $"(занятие ID={existing.Id})"
                );
            }

            if (existing.GroupId == newSession.GroupId)
            {
                var group = GetGroup(existing.GroupId);
                conflicts.Add(
                    $"Группа {group?.Code} занята " +
                    $"{existing.StartTime}-{existing.EndTime} " +
                    $"(занятие ID={existing.Id})"
                );
            }
        }

        return conflicts;
    }

    public Session? GetSession(int id)
    {
        return _sessions.FirstOrDefault(s => s.Id == id);
    }

    public bool DeleteSession(int id)
    {
        var session = GetSession(id);
        if (session == null)
        {
            return false;
        }

        _sessions.Remove(session);
        return true;
    }

    public List<Session> GetSessionsByGroup(int groupId)
    {
        return _sessions.Where(s => s.GroupId == groupId).ToList();
    }

    public List<Session> GetSessionsByTeacher(int teacherId)
    {
        return _sessions.Where(s => s.TeacherId == teacherId).ToList();
    }

    public List<Session> GetSessionsByRoom(int roomId)
    {
        return _sessions.Where(s => s.RoomId == roomId).ToList();
    }

    public List<Session> GetSessionsByDate(DateTime date)
    {
        return _sessions.Where(s => s.Date.Date == date.Date).ToList();
    }

    public List<Session> GetSessionsByDateRange(DateTime from, DateTime to)
    {
        return _sessions
            .Where(s => s.Date.Date >= from.Date && s.Date.Date <= to.Date)
            .ToList();
    }

    private class DatabaseSnapshot
    {
        public List<Room> Rooms { get; set; } = new();
        public List<Teacher> Teachers { get; set; } = new();
        public List<Group> Groups { get; set; } = new();
        public List<Course> Courses { get; set; } = new();
        public List<Session> Sessions { get; set; } = new();
        public int NextRoomId { get; set; }
        public int NextTeacherId { get; set; }
        public int NextGroupId { get; set; }
        public int NextCourseId { get; set; }
        public int NextSessionId { get; set; }
    }
}
