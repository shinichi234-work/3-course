namespace HM10.Commands;

public static class SessionCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        if (args.Length < 2)
        {
            Console.WriteLine(
                "Использование: session <add|list|show|delete|find-conflicts>"
            );
            return;
        }

        var action = args[1].ToLower();

        switch (action)
        {
            case "add":
                Add(args, db);
                break;
            case "list":
                List(args, db);
                break;
            case "show":
                Show(args, db);
                break;
            case "delete":
                Delete(args, db);
                break;
            case "find-conflicts":
                FindConflicts(args, db);
                break;
            default:
                Console.WriteLine($"Неизвестное действие: {action}");
                break;
        }
    }

    private static void Add(string[] args, Storage.Database db)
    {
        int? courseId = null;
        int? teacherId = null;
        int? groupId = null;
        int? roomId = null;
        DateTime? date = null;
        TimeSpan? startTime = null;
        TimeSpan? endTime = null;
        string notes = "";
        bool force = false;

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--course" && i + 1 < args.Length)
            {
                courseId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--teacher" && i + 1 < args.Length)
            {
                teacherId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--group" && i + 1 < args.Length)
            {
                groupId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--room" && i + 1 < args.Length)
            {
                roomId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--date" && i + 1 < args.Length)
            {
                date = DateTime.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--start" && i + 1 < args.Length)
            {
                startTime = TimeSpan.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--end" && i + 1 < args.Length)
            {
                endTime = TimeSpan.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--notes" && i + 1 < args.Length)
            {
                notes = args[i + 1];
                i++;
            }
            else if (args[i] == "--force")
            {
                force = true;
            }
        }

        if (!courseId.HasValue || !teacherId.HasValue || 
            !groupId.HasValue || !roomId.HasValue || 
            !date.HasValue || !startTime.HasValue || !endTime.HasValue)
        {
            Console.WriteLine(
                "Требуются параметры: --course --teacher --group " +
                "--room --date --start --end"
            );
            return;
        }

        var session = db.AddSession(
            courseId.Value,
            teacherId.Value,
            groupId.Value,
            roomId.Value,
            date.Value,
            startTime.Value,
            endTime.Value,
            notes,
            force
        );

        db.Save();

        var course = db.GetCourse(session.CourseId);
        var teacher = db.GetTeacher(session.TeacherId);
        var group = db.GetGroup(session.GroupId);
        var room = db.GetRoom(session.RoomId);

        Console.WriteLine(
            $"Занятие создано: id={session.Id}, " +
            $"{session.Date:yyyy-MM-dd} {session.StartTime}-{session.EndTime}, " +
            $"Аудитория {room?.Code}, Преподаватель {teacher?.Name}, " +
            $"Группа {group?.Code}"
        );
    }

    private static void List(string[] args, Storage.Database db)
    {
        int? groupId = null;
        int? teacherId = null;
        int? roomId = null;
        DateTime? date = null;
        DateTime? from = null;
        DateTime? to = null;

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--group" && i + 1 < args.Length)
            {
                groupId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--teacher" && i + 1 < args.Length)
            {
                teacherId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--room" && i + 1 < args.Length)
            {
                roomId = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--date" && i + 1 < args.Length)
            {
                date = DateTime.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--from" && i + 1 < args.Length)
            {
                from = DateTime.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--to" && i + 1 < args.Length)
            {
                to = DateTime.Parse(args[i + 1]);
                i++;
            }
        }

        List<Models.Session> sessions;

        if (groupId.HasValue)
        {
            sessions = db.GetSessionsByGroup(groupId.Value);
        }
        else if (teacherId.HasValue)
        {
            sessions = db.GetSessionsByTeacher(teacherId.Value);
        }
        else if (roomId.HasValue)
        {
            sessions = db.GetSessionsByRoom(roomId.Value);
        }
        else if (date.HasValue)
        {
            sessions = db.GetSessionsByDate(date.Value);
        }
        else if (from.HasValue && to.HasValue)
        {
            sessions = db.GetSessionsByDateRange(from.Value, to.Value);
        }
        else
        {
            sessions = db.Sessions.ToList();
        }

        if (sessions.Count == 0)
        {
            Console.WriteLine("Занятий нет");
            return;
        }

        Console.WriteLine(
            "ID | Дата       | Время       | Курс | Преподаватель | " +
            "Группа | Аудитория"
        );
        Console.WriteLine(
            "---|------------|-------------|------|---------------|" +
            "--------|----------"
        );

        foreach (var session in sessions)
        {
            var course = db.GetCourse(session.CourseId);
            var teacher = db.GetTeacher(session.TeacherId);
            var group = db.GetGroup(session.GroupId);
            var room = db.GetRoom(session.RoomId);

            Console.WriteLine(
                $"{session.Id,-3}| " +
                $"{session.Date:yyyy-MM-dd} | " +
                $"{session.StartTime}-{session.EndTime} | " +
                $"{course?.Title,-5}| " +
                $"{teacher?.Name,-14}| " +
                $"{group?.Code,-7}| " +
                $"{room?.Code}"
            );
        }
    }

    private static void Show(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: session show <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        var session = db.GetSession(id);
        if (session == null)
        {
            Console.WriteLine($"Занятие с ID {id} не найдено");
            return;
        }

        var course = db.GetCourse(session.CourseId);
        var teacher = db.GetTeacher(session.TeacherId);
        var group = db.GetGroup(session.GroupId);
        var room = db.GetRoom(session.RoomId);

        Console.WriteLine($"ID: {session.Id}");
        Console.WriteLine($"Дата: {session.Date:yyyy-MM-dd}");
        Console.WriteLine(
            $"Время: {session.StartTime} - {session.EndTime}"
        );
        Console.WriteLine($"Курс: {course?.Title}");
        Console.WriteLine($"Преподаватель: {teacher?.Name}");
        Console.WriteLine($"Группа: {group?.Code}");
        Console.WriteLine($"Аудитория: {room?.Code}");
        Console.WriteLine($"Заметки: {session.Notes}");
    }

    private static void Delete(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: session delete <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        if (db.DeleteSession(id))
        {
            db.Save();
            Console.WriteLine($"Занятие {id} удалено");
        }
        else
        {
            Console.WriteLine($"Занятие {id} не найдено");
        }
    }

    private static void FindConflicts(string[] args, Storage.Database db)
    {
        DateTime? from = null;
        DateTime? to = null;

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--from" && i + 1 < args.Length)
            {
                from = DateTime.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--to" && i + 1 < args.Length)
            {
                to = DateTime.Parse(args[i + 1]);
                i++;
            }
        }

        var sessions = from.HasValue && to.HasValue
            ? db.GetSessionsByDateRange(from.Value, to.Value)
            : db.Sessions.ToList();

        var allConflicts = new List<string>();

        foreach (var session in sessions)
        {
            var conflicts = db.FindConflicts(session);
            if (conflicts.Count > 0)
            {
                allConflicts.Add(
                    $"Занятие ID={session.Id} ({session.Date:yyyy-MM-dd} " +
                    $"{session.StartTime}-{session.EndTime}):"
                );
                allConflicts.AddRange(conflicts.Select(c => $"  - {c}"));
            }
        }

        if (allConflicts.Count == 0)
        {
            Console.WriteLine("Конфликтов не найдено");
        }
        else
        {
            Console.WriteLine("Найдены конфликты:");
            foreach (var conflict in allConflicts)
            {
                Console.WriteLine(conflict);
            }
        }
    }
}