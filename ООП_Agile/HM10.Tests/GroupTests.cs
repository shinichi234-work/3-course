namespace HM10.Tests;

using HM10.Storage;
using Xunit;

public class GroupTests
{
    [Fact]
    public void AddGroup_ValidData_Success()
    {
        var db = new Database();
        var group = db.AddGroup("CS-2025", 30, 2025);

        Assert.Equal(1, group.Id);
        Assert.Equal("CS-2025", group.Code);
        Assert.Equal(30, group.Size);
        Assert.Equal(2025, group.Year);
    }

    [Fact]
    public void AddGroup_DuplicateCode_ThrowsException()
    {
        var db = new Database();
        db.AddGroup("CS-2025", 30, 2025);

        Assert.Throws<ArgumentException>(
            () => db.AddGroup("CS-2025", 25, 2025)
        );
    }

    [Fact]
    public void AddGroup_InvalidSize_ThrowsException()
    {
        var db = new Database();

        Assert.Throws<ArgumentException>(
            () => db.AddGroup("CS-2025", 0, 2025)
        );
        Assert.Throws<ArgumentException>(
            () => db.AddGroup("CS-2026", 150, 2025)
        );
    }

    [Fact]
    public void GetGroupByCode_ExistingCode_ReturnsGroup()
    {
        var db = new Database();
        db.AddGroup("CS-2025", 30, 2025);

        var found = db.GetGroupByCode("CS-2025");

        Assert.NotNull(found);
        Assert.Equal(30, found.Size);
    }

    [Fact]
    public void UpdateGroup_ValidData_Success()
    {
        var db = new Database();
        var group = db.AddGroup("CS-2025", 30, 2025);

        var result = db.UpdateGroup(group.Id, size: 35);

        Assert.True(result);
        var updated = db.GetGroup(group.Id);
        Assert.Equal(35, updated?.Size);
    }

    [Fact]
    public void DeleteGroup_NoSessions_Success()
    {
        var db = new Database();
        var group = db.AddGroup("CS-2025", 30, 2025);

        var result = db.DeleteGroup(group.Id);

        Assert.True(result);
        Assert.Null(db.GetGroup(group.Id));
    }
}