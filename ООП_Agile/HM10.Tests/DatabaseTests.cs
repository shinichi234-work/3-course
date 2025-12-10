namespace HM10.Tests;

using HM10.Storage;
using Xunit;

public class DatabaseTests
{
    [Fact]
    public void SaveAndLoad_PreservesData()
    {
        var db = new Database();
        var testPath = "test_db.json";

        db.AddRoom("A-101", 30);
        db.AddTeacher("Ivanov");
        db.Save(testPath);

        var db2 = new Database();
        db2.Load(testPath);

        Assert.Single(db2.Rooms);
        Assert.Single(db2.Teachers);
        Assert.Equal("A-101", db2.Rooms[0].Code);

        if (File.Exists(testPath))
        {
            File.Delete(testPath);
        }
    }

    [Fact]
    public void Initialize_CreatesNewDatabase()
    {
        var testPath = "new_db.json";

        if (File.Exists(testPath))
        {
            File.Delete(testPath);
        }

        var db = new Database();
        db.Initialize(testPath);

        Assert.True(File.Exists(testPath));

        File.Delete(testPath);
    }

    [Fact]
    public void Initialize_ExistingDatabase_ThrowsException()
    {
        var testPath = "existing_db.json";
        File.WriteAllText(testPath, "{}");

        var db = new Database();

        Assert.Throws<InvalidOperationException>(
            () => db.Initialize(testPath)
        );

        File.Delete(testPath);
    }
}