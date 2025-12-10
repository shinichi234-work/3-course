using HM10.Storage;
using HM10.Commands;

namespace HM10;

public class Program
{
    public static void Main(string[] args)
    {
        var db = new Database();

        if (File.Exists("schedule_db.json") && 
            (args.Length == 0 || args[0] != "init"))
        {
            db.Load();
        }

        CommandParser.Execute(args, db);
    }
}
