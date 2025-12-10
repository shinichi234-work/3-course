namespace HM10.Tests;

using HM10.Storage;
using Xunit;

public class CourseTests
{
    [Fact]
    public void AddCourse_ValidData_Success()
    {
        var db = new Database();
        var course = db.AddCourse("Mathematics", "MATH101", 90);

        Assert.Equal(1, course.Id);
        Assert.Equal("Mathematics", course.Title);
        Assert.Equal("MATH101", course.Code);
        Assert.Equal(90, course.Duration);
    }

    [Fact]
    public void AddCourse_EmptyTitle_ThrowsException()
    {
        var db = new Database();

        Assert.Throws<ArgumentException>(() => db.AddCourse(""));
    }

    [Fact]
    public void AddCourse_InvalidDuration_ThrowsException()
    {
        var db = new Database();

        Assert.Throws<ArgumentException>(
            () => db.AddCourse("Math", "", 20)
        );
        Assert.Throws<ArgumentException>(
            () => db.AddCourse("Math", "", 300)
        );
    }

    [Fact]
    public void GetCourse_ExistingId_ReturnsCourse()
    {
        var db = new Database();
        var course = db.AddCourse("Mathematics");

        var found = db.GetCourse(course.Id);

        Assert.NotNull(found);
        Assert.Equal("Mathematics", found.Title);
    }

    [Fact]
    public void UpdateCourse_ValidData_Success()
    {
        var db = new Database();
        var course = db.AddCourse("Mathematics");

        var result = db.UpdateCourse(course.Id, title: "Advanced Math");

        Assert.True(result);
        var updated = db.GetCourse(course.Id);
        Assert.Equal("Advanced Math", updated?.Title);
    }

    [Fact]
    public void DeleteCourse_NoSessions_Success()
    {
        var db = new Database();
        var course = db.AddCourse("Mathematics");

        var result = db.DeleteCourse(course.Id);

        Assert.True(result);
        Assert.Null(db.GetCourse(course.Id));
    }
}