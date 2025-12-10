namespace HM10.Tests;

using HM10.Storage;
using Xunit;

public class TeacherTests
{
    [Fact]
    public void AddTeacher_ValidData_Success()
    {
        var db = new Database();
        var teacher = db.AddTeacher("Ivanov I.I.", "ivanov@test.com");

        Assert.Equal(1, teacher.Id);
        Assert.Equal("Ivanov I.I.", teacher.Name);
        Assert.Equal("ivanov@test.com", teacher.Email);
    }

    [Fact]
    public void AddTeacher_EmptyName_ThrowsException()
    {
        var db = new Database();

        Assert.Throws<ArgumentException>(() => db.AddTeacher(""));
    }

    [Fact]
    public void AddTeacher_InvalidEmail_ThrowsException()
    {
        var db = new Database();

        Assert.Throws<ArgumentException>(
            () => db.AddTeacher("Ivanov", "invalid-email")
        );
    }

    [Fact]
    public void GetTeacher_ExistingId_ReturnsTeacher()
    {
        var db = new Database();
        var teacher = db.AddTeacher("Ivanov");

        var found = db.GetTeacher(teacher.Id);

        Assert.NotNull(found);
        Assert.Equal("Ivanov", found.Name);
    }

    [Fact]
    public void UpdateTeacher_ValidData_Success()
    {
        var db = new Database();
        var teacher = db.AddTeacher("Ivanov");

        var result = db.UpdateTeacher(teacher.Id, name: "Petrov");

        Assert.True(result);
        var updated = db.GetTeacher(teacher.Id);
        Assert.Equal("Petrov", updated?.Name);
    }

    [Fact]
    public void DeleteTeacher_NoSessions_Success()
    {
        var db = new Database();
        var teacher = db.AddTeacher("Ivanov");

        var result = db.DeleteTeacher(teacher.Id);

        Assert.True(result);
        Assert.Null(db.GetTeacher(teacher.Id));
    }
}