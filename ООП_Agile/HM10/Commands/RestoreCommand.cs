namespace HM10.Commands;

public static class RestoreCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        string? inputPath = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--from" && i + 1 < args.Length)
            {
                inputPath = args[i + 1];
                i++;
            }
        }

        if (inputPath == null)
        {
            Console.WriteLine("Требуется параметр: --from <путь>");
            return;
        }

        db.Load(inputPath);
        db.Save();
        Console.WriteLine($"База данных восстановлена из: {inputPath}");
    }
}
