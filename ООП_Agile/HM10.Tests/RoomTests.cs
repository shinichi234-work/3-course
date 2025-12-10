namespace HM10.Tests;

using HM10.Storage;
using Xunit;

public class RoomTests
{
    [Fact]
    public void AddRoom_ValidData_Success()
    {
        var db = new Database();
        var room = db.AddRoom("A-101", 30, "Main");

        Assert.Equal(1, room.Id);
        Assert.Equal("A-101", room.Code);
        Assert.Equal(30, room.Capacity);
        Assert.Equal("Main", room.Building);
    }

    [Fact]
    public void AddRoom_DuplicateCode_ThrowsException()
    {
        var db = new Database();
        db.AddRoom("A-101", 30);

        Assert.Throws<ArgumentException>(() => db.AddRoom("A-101", 40));
    }

    [Fact]
    public void AddRoom_InvalidCapacity_ThrowsException()
    {
        var db = new Database();

        Assert.Throws<ArgumentException>(() => db.AddRoom("A-101", 0));
        Assert.Throws<ArgumentException>(() => db.AddRoom("A-102", 600));
    }

    [Fact]
    public void GetRoom_ExistingId_ReturnsRoom()
    {
        var db = new Database();
        var room = db.AddRoom("A-101", 30);

        var found = db.GetRoom(room.Id);

        Assert.NotNull(found);
        Assert.Equal("A-101", found.Code);
    }

    [Fact]
    public void GetRoom_NonExistingId_ReturnsNull()
    {
        var db = new Database();

        var found = db.GetRoom(999);

        Assert.Null(found);
    }

    [Fact]
    public void UpdateRoom_ValidData_Success()
    {
        var db = new Database();
        var room = db.AddRoom("A-101", 30);

        var result = db.UpdateRoom(room.Id, capacity: 50);

        Assert.True(result);
        var updated = db.GetRoom(room.Id);
        Assert.Equal(50, updated?.Capacity);
    }

    [Fact]
    public void DeleteRoom_NoSessions_Success()
    {
        var db = new Database();
        var room = db.AddRoom("A-101", 30);

        var result = db.DeleteRoom(room.Id);

        Assert.True(result);
        Assert.Null(db.GetRoom(room.Id));
    }

    [Fact]
    public void DeleteRoom_WithSessions_ThrowsException()
    {
        var db = new Database();
        var room = db.AddRoom("A-101", 30);
        var teacher = db.AddTeacher("Ivanov");
        var group = db.AddGroup("CS-2025", 20, 2025);
        var course = db.AddCourse("Math");

        db.AddSession(
            course.Id,
            teacher.Id,
            group.Id,
            room.Id,
            DateTime.Now,
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        Assert.Throws<InvalidOperationException>(
            () => db.DeleteRoom(room.Id)
        );
    }
}