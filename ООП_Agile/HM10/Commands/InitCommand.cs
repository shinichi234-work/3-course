namespace HM10.Commands;

public static class InitCommand
{
    public static void Execute(string[] args, Storage.Database db)
    {
        var path = "schedule_db.json";

        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] == "--db" && i + 1 < args.Length)
            {
                path = args[i + 1];
                i++;
            }
        }

        db.Initialize(path);
    }
}
