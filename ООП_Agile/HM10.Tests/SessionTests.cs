namespace HM10.Tests;

using HM10.Storage;
using Xunit;

public class SessionTests
{
    private Database CreateDbWithEntities()
    {
        var db = new Database();
        db.AddRoom("A-101", 30);
        db.AddTeacher("Ivanov");
        db.AddGroup("CS-2025", 20, 2025);
        db.AddCourse("Mathematics");
        return db;
    }

    [Fact]
    public void AddSession_ValidData_Success()
    {
        var db = CreateDbWithEntities();

        var session = db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        Assert.Equal(1, session.Id);
        Assert.Equal(1, session.CourseId);
        Assert.Equal(1, session.TeacherId);
    }

    [Fact]
    public void AddSession_InvalidTime_ThrowsException()
    {
        var db = CreateDbWithEntities();

        Assert.Throws<ArgumentException>(() => db.AddSession(
            1, 1, 1, 1,
            DateTime.Now,
            new TimeSpan(11, 0, 0),
            new TimeSpan(10, 0, 0)
        ));
    }

    [Fact]
    public void AddSession_RoomConflict_ThrowsException()
    {
        var db = CreateDbWithEntities();

        db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        Assert.Throws<InvalidOperationException>(() => db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(11, 0, 0),
            new TimeSpan(12, 0, 0)
        ));
    }

    [Fact]
    public void AddSession_TeacherConflict_ThrowsException()
    {
        var db = CreateDbWithEntities();
        db.AddRoom("A-102", 30);

        db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        Assert.Throws<InvalidOperationException>(() => db.AddSession(
            1, 1, 1, 2,
            new DateTime(2025, 11, 26),
            new TimeSpan(11, 0, 0),
            new TimeSpan(12, 0, 0)
        ));
    }

    [Fact]
    public void AddSession_GroupConflict_ThrowsException()
    {
        var db = CreateDbWithEntities();
        db.AddRoom("A-102", 30);
        db.AddTeacher("Petrov");

        db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        Assert.Throws<InvalidOperationException>(() => db.AddSession(
            1, 2, 1, 2,
            new DateTime(2025, 11, 26),
            new TimeSpan(11, 0, 0),
            new TimeSpan(12, 0, 0)
        ));
    }

    [Fact]
    public void AddSession_NoConflictDifferentDay_Success()
    {
        var db = CreateDbWithEntities();

        db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        var session = db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 27),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        Assert.NotNull(session);
    }

    [Fact]
    public void AddSession_WithForce_IgnoresConflicts()
    {
        var db = CreateDbWithEntities();

        db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        var session = db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(11, 0, 0),
            new TimeSpan(12, 0, 0),
            "",
            force: true
        );

        Assert.NotNull(session);
    }

    [Fact]
    public void GetSessionsByGroup_ReturnsCorrectSessions()
    {
        var db = CreateDbWithEntities();
        db.AddGroup("CS-2026", 25, 2026);

        db.AddSession(
            1, 1, 1, 1,
            DateTime.Now,
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        db.AddSession(
            1, 1, 2, 1,
            DateTime.Now,
            new TimeSpan(13, 0, 0),
            new TimeSpan(14, 30, 0)
        );

        var sessions = db.GetSessionsByGroup(1);

        Assert.Single(sessions);
        Assert.Equal(1, sessions[0].GroupId);
    }

    [Fact]
    public void DeleteSession_ExistingSession_Success()
    {
        var db = CreateDbWithEntities();

        var session = db.AddSession(
            1, 1, 1, 1,
            DateTime.Now,
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        var result = db.DeleteSession(session.Id);

        Assert.True(result);
        Assert.Null(db.GetSession(session.Id));
    }

    [Fact]
    public void FindConflicts_ReturnsAllConflictTypes()
    {
        var db = CreateDbWithEntities();

        db.AddSession(
            1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(10, 0, 0),
            new TimeSpan(11, 30, 0)
        );

        var newSession = new HM10.Models.Session(
            0, 1, 1, 1, 1,
            new DateTime(2025, 11, 26),
            new TimeSpan(11, 0, 0),
            new TimeSpan(12, 0, 0)
        );

        var conflicts = db.FindConflicts(newSession);

        Assert.Equal(3, conflicts.Count);
        Assert.Contains(conflicts, c => c.Contains("Аудитория"));
        Assert.Contains(conflicts, c => c.Contains("Преподаватель"));
        Assert.Contains(conflicts, c => c.Contains("Группа"));
    }
}