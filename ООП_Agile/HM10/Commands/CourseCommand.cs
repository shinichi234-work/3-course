namespace HM10.Commands;

public static class CourseCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        if (args.Length < 2)
        {
            Console.WriteLine(
                "Использование: course <add|list|show|update|delete>"
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
                List(db);
                break;
            case "show":
                Show(args, db);
                break;
            case "update":
                Update(args, db);
                break;
            case "delete":
                Delete(args, db);
                break;
            default:
                Console.WriteLine($"Неизвестное действие: {action}");
                break;
        }
    }

    private static void Add(string[] args, Storage.Database db)
    {
        string? title = null;
        string code = "";
        int duration = 90;

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--title" && i + 1 < args.Length)
            {
                title = args[i + 1];
                i++;
            }
            else if (args[i] == "--code" && i + 1 < args.Length)
            {
                code = args[i + 1];
                i++;
            }
            else if (args[i] == "--duration" && i + 1 < args.Length)
            {
                duration = int.Parse(args[i + 1]);
                i++;
            }
        }

        if (title == null)
        {
            Console.WriteLine("Требуется параметр: --title");
            return;
        }

        var course = db.AddCourse(title, code, duration);
        db.Save();
        Console.WriteLine(
            $"Курс {course.Title} (id={course.Id}) создан."
        );
    }

    private static void List(Storage.Database db)
    {
        var courses = db.Courses;

        if (courses.Count == 0)
        {
            Console.WriteLine("Курсов нет");
            return;
        }

        Console.WriteLine("ID | Название                     | Код      | Длит.");
        Console.WriteLine("---|------------------------------|----------|-------");

        foreach (var course in courses)
        {
            Console.WriteLine(
                $"{course.Id,-3}| {course.Title,-29}| " +
                $"{course.Code,-9}| {course.Duration}"
            );
        }
    }

    private static void Show(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: course show <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        var course = db.GetCourse(id);
        if (course != null)
        {
            Console.WriteLine($"ID: {course.Id}");
            Console.WriteLine($"Название: {course.Title}");
            Console.WriteLine($"Код: {course.Code}");
            Console.WriteLine($"Длительность: {course.Duration} минут");
        }
        else
        {
            Console.WriteLine($"Курс с ID {id} не найден");
        }
    }

    private static void Update(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: course update <id> [параметры]");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        string? title = null;
        string? code = null;
        int? duration = null;

        for (int i = 3; i < args.Length; i++)
        {
            if (args[i] == "--title" && i + 1 < args.Length)
            {
                title = args[i + 1];
                i++;
            }
            else if (args[i] == "--code" && i + 1 < args.Length)
            {
                code = args[i + 1];
                i++;
            }
            else if (args[i] == "--duration" && i + 1 < args.Length)
            {
                duration = int.Parse(args[i + 1]);
                i++;
            }
        }

        if (db.UpdateCourse(id, title, code, duration))
        {
            db.Save();
            Console.WriteLine($"Курс {id} обновлён");
        }
        else
        {
            Console.WriteLine($"Курс {id} не найден");
        }
    }

    private static void Delete(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: course delete <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        if (db.DeleteCourse(id))
        {
            db.Save();
            Console.WriteLine($"Курс {id} удалён");
        }
        else
        {
            Console.WriteLine($"Курс {id} не найден");
        }
    }
}
