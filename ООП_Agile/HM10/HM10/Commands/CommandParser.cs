namespace HM10.Commands;

public static class CommandParser
{
    public static void Execute(string[] args, Storage.Database db)
    {
        if (args.Length == 0)
        {
            ShowHelp();
            return;
        }

        var command = args[0].ToLower();

        try
        {
            switch (command)
            {
                case "init":
                    InitCommand.Execute(args, db);
                    break;
                case "room":
                    RoomCommand.Execute(args, db);
                    break;
                case "teacher":
                    TeacherCommand.Execute(args, db);
                    break;
                case "group":
                    GroupCommand.Execute(args, db);
                    break;
                case "course":
                    CourseCommand.Execute(args, db);
                    break;
                case "session":
                    SessionCommand.Execute(args, db);
                    break;
                case "backup":
                    BackupCommand.Execute(args, db);
                    break;
                case "restore":
                    RestoreCommand.Execute(args, db);
                    break;
                case "help":
                    ShowHelp();
                    break;
                default:
                    Console.WriteLine($"Неизвестная команда: {command}");
                    Console.WriteLine("Используйте 'help' для справки");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ОШИБКА: {ex.Message}");
            Environment.Exit(1);
        }
    }

    private static void ShowHelp()
    {
        Console.WriteLine("Система управления расписанием");
        Console.WriteLine();
        Console.WriteLine("Команды:");
        Console.WriteLine("  init                  - Создать новую базу данных");
        Console.WriteLine("  room <action>         - Управление аудиториями");
        Console.WriteLine("  teacher <action>      - Управление преподавателями");
        Console.WriteLine("  group <action>        - Управление группами");
        Console.WriteLine("  course <action>       - Управление курсами");
        Console.WriteLine("  session <action>      - Управление занятиями");
        Console.WriteLine("  backup --out <path>   - Создать резервную копию");
        Console.WriteLine("  restore --from <path> - Восстановить из копии");
        Console.WriteLine("  help                  - Показать эту справку");
        Console.WriteLine();
        Console.WriteLine("Действия (action):");
        Console.WriteLine("  add     - Добавить");
        Console.WriteLine("  list    - Показать список");
        Console.WriteLine("  show    - Показать детали");
        Console.WriteLine("  update  - Обновить");
        Console.WriteLine("  delete  - Удалить");
    }
}