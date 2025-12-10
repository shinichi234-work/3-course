namespace HM10.Commands;

public static class BackupCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        string? outputPath = null;

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--out" && i + 1 < args.Length)
            {
                outputPath = args[i + 1];
                i++;
            }
        }

        if (outputPath == null)
        {
            Console.WriteLine("Требуется параметр: --out <путь>");
            return;
        }

        db.Save(outputPath);
        Console.WriteLine($"Резервная копия создана: {outputPath}");
    }
}