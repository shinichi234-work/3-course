namespace HM10.Commands;

public static class TeacherCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        if (args.Length < 2)
        {
            Console.WriteLine(
                "Использование: teacher <add|list|show|update|delete>"
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
        string? name = null;
        string email = "";

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--name" && i + 1 < args.Length)
            {
                name = args[i + 1];
                i++;
            }
            else if (args[i] == "--email" && i + 1 < args.Length)
            {
                email = args[i + 1];
                i++;
            }
        }

        if (name == null)
        {
            Console.WriteLine("Требуется параметр: --name");
            return;
        }

        var teacher = db.AddTeacher(name, email);
        db.Save();
        Console.WriteLine(
            $"Преподаватель {teacher.Name} (id={teacher.Id}) создан."
        );
    }

    private static void List(Storage.Database db)
    {
        var teachers = db.Teachers;

        if (teachers.Count == 0)
        {
            Console.WriteLine("Преподавателей нет");
            return;
        }

        Console.WriteLine("ID | Имя                          | Email");
        Console.WriteLine("---|------------------------------|--------");

        foreach (var teacher in teachers)
        {
            Console.WriteLine(
                $"{teacher.Id,-3}| {teacher.Name,-29}| {teacher.Email}"
            );
        }
    }

    private static void Show(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: teacher show <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        var teacher = db.GetTeacher(id);
        if (teacher != null)
        {
            Console.WriteLine($"ID: {teacher.Id}");
            Console.WriteLine($"Имя: {teacher.Name}");
            Console.WriteLine($"Email: {teacher.Email}");
        }
        else
        {
            Console.WriteLine($"Преподаватель с ID {id} не найден");
        }
    }

    private static void Update(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine(
                "Использование: teacher update <id> [параметры]"
            );
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        string? name = null;
        string? email = null;

        for (int i = 3; i < args.Length; i++)
        {
            if (args[i] == "--name" && i + 1 < args.Length)
            {
                name = args[i + 1];
                i++;
            }
            else if (args[i] == "--email" && i + 1 < args.Length)
            {
                email = args[i + 1];
                i++;
            }
        }

        if (db.UpdateTeacher(id, name, email))
        {
            db.Save();
            Console.WriteLine($"Преподаватель {id} обновлён");
        }
        else
        {
            Console.WriteLine($"Преподаватель {id} не найден");
        }
    }

    private static void Delete(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: teacher delete <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        if (db.DeleteTeacher(id))
        {
            db.Save();
            Console.WriteLine($"Преподаватель {id} удалён");
        }
        else
        {
            Console.WriteLine($"Преподаватель {id} не найден");
        }
    }
}
