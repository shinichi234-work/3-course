namespace HM10.Commands;

public static class RoomCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Использование: room <add|list|show|update|delete>");
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
        int? capacity = null;
        string building = "";

        for (int i = 2; i < args.Length; i++)
        {
            if (args[i] == "--code" && i + 1 < args.Length)
            {
                code = args[i + 1];
                i++;
            }
            else if (args[i] == "--capacity" && i + 1 < args.Length)
            {
                capacity = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--building" && i + 1 < args.Length)
            {
                building = args[i + 1];
                i++;
            }
        }

        if (code == null || !capacity.HasValue)
        {
            Console.WriteLine("Требуются параметры: --code --capacity");
            return;
        }

        var room = db.AddRoom(code, capacity.Value, building);
        db.Save();
        Console.WriteLine(
            $"Аудитория {room.Code} (id={room.Id}) создана."
        );
    }

    private static void List(Storage.Database db)
    {
        var rooms = db.Rooms;

        if (rooms.Count == 0)
        {
            Console.WriteLine("Аудиторий нет");
            return;
        }

        Console.WriteLine("ID | Код      | Вместимость | Корпус");
        Console.WriteLine("---|----------|-------------|--------");

        foreach (var room in rooms)
        {
            Console.WriteLine(
                $"{room.Id,-3}| {room.Code,-9}| {room.Capacity,-12}| {room.Building}"
            );
        }
    }

    private static void Show(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: room show <id>");
            return;
        }

        if (int.TryParse(args[2], out int id))
        {
            var room = db.GetRoom(id);
            if (room != null)
            {
                Console.WriteLine($"ID: {room.Id}");
                Console.WriteLine($"Код: {room.Code}");
                Console.WriteLine($"Вместимость: {room.Capacity}");
                Console.WriteLine($"Корпус: {room.Building}");
            }
            else
            {
                Console.WriteLine($"Аудитория с ID {id} не найдена");
            }
        }
        else
        {
            var room = db.GetRoomByCode(args[2]);
            if (room != null)
            {
                Console.WriteLine($"ID: {room.Id}");
                Console.WriteLine($"Код: {room.Code}");
                Console.WriteLine($"Вместимость: {room.Capacity}");
                Console.WriteLine($"Корпус: {room.Building}");
            }
            else
            {
                Console.WriteLine($"Аудитория {args[2]} не найдена");
            }
        }
    }

    private static void Update(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: room update <id> [параметры]");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        string? code = null;
        int? capacity = null;
        string? building = null;

        for (int i = 3; i < args.Length; i++)
        {
            if (args[i] == "--code" && i + 1 < args.Length)
            {
                code = args[i + 1];
                i++;
            }
            else if (args[i] == "--capacity" && i + 1 < args.Length)
            {
                capacity = int.Parse(args[i + 1]);
                i++;
            }
            else if (args[i] == "--building" && i + 1 < args.Length)
            {
                building = args[i + 1];
                i++;
            }
        }

        if (db.UpdateRoom(id, code, capacity, building))
        {
            db.Save();
            Console.WriteLine($"Аудитория {id} обновлена");
        }
        else
        {
            Console.WriteLine($"Аудитория {id} не найдена");
        }
    }

    private static void Delete(string[] args, Storage.Database db)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Использование: room delete <id>");
            return;
        }

        if (!int.TryParse(args[2], out int id))
        {
            Console.WriteLine("Некорректный ID");
            return;
        }

        if (db.DeleteRoom(id))
        {
            db.Save();
            Console.WriteLine($"Аудитория {id} удалена");
        }
        else
        {
            Console.WriteLine($"Аудитория {id} не найдена");
        }
    }
}