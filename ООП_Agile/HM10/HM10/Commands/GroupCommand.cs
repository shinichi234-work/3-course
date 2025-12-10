namespace HM10.Commands;

public static class GroupCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        if (args.Length < 2)
        {
            Console.WriteLine(
                "Использование: group <add|list|show|update|delete>"
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
        string? code = null;
        int? size = null;
        int year = 0;

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--code" && i + 1 < args.Length)
            {
                code = args[i + 1];
                i++;
            }
            else if (args[i] == "--size" && i + 1 < args.Length)
            {
                size = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--year" && i + 1 < args.Length)
            {
                year = int.Parse(args[i + 1]);
                i++;
            }
        }

        if (code == null || !size.HasValue)
        {
            Console.WriteLine("Требуются параметры: --code --size");
            return;
        }

        var group = db.AddGroup(code, size.Value, year);
        db.Save();
        Console.WriteLine($"Группа {group.Code} (id={group.Id}) создана.");
    }

    private static void List(Storage.Database db)
    {
        var groups = db.Groups;

        if (groups.Count == 0)
        {
            Console.WriteLine("Групп нет");
            return;
        }

        Console.WriteLine("ID | Код      | Размер | Год");
        Console.WriteLine("---|----------|--------|------");

        foreach (var group in groups)
        {
            Console.WriteLine(
                $"{group.Id,-3}| {group.Code,-9}| {group.Size,-7}| " +
                $"{group.Year}"
            );
        }
    }

    private static void Show(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: group show <id>");
            return;
        }

        if (int.TryParse(args[2], out int id))
        {
            var group = db.GetGroup(id);
            if (group != null)
            {
                Console.WriteLine($"ID: {group.Id}");
                Console.WriteLine($"Код: {group.Code}");
                Console.WriteLine($"Размер: {group.Size}");
                Console.WriteLine($"Год: {group.Year}");
            }
            else
            {
                Console.WriteLine($"Группа с ID {id} не найдена");
            }
        }
        else
        {
            var group = db.GetGroupByCode(args[2]);
            if (group != null)
            {
                Console.WriteLine($"ID: {group.Id}");
                Console.WriteLine($"Код: {group.Code}");
                Console.WriteLine($"Размер: {group.Size}");
                Console.WriteLine($"Год: {group.Year}");
            }
            else
            {
                Console.WriteLine($"Группа {args[2]} не найдена");
            }
        }
    }

    private static void Update(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: group update <id> [параметры]");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        string? code = null;
        int? size = null;
        int? year = null;

        for (int i = 3; i < args.Length; i++)
        {
            if (args[i] == "--code" && i + 1 < args.Length)
            {
                code = args[i + 1];
                i++;
            }
            else if (args[i] == "--size" && i + 1 < args.Length)
            {
                size = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--year" && i + 1 < args.Length)
            {
                year = int.Parse(args[i + 1]);
                i++;
            }
        }

        if (db.UpdateGroup(id, code, size, year))
        {
            db.Save();
            Console.WriteLine($"Группа {id} обновлена");
        }
        else
        {
            Console.WriteLine($"Группа {id} не найдена");
        }
    }

    private static void Delete(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: group delete <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        if (db.DeleteGroup(id))
        {
            db.Save();
            Console.WriteLine($"Группа {id} удалена");
        }
        else
        {
            Console.WriteLine($"Группа {id} не найдена");
        }
    }
}