namespace HM1;

public class Subject
{
    private const int InitialStudentCount = 0;
    
    public string Name { get; }
    public string Teacher { get; }
    public int StudentCount { get; private set; }

    public Subject(string name, string teacher)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Имя прдмета не может быть пустым", 
                nameof(name)
            );
        }
        
        if (string.IsNullOrWhiteSpace(teacher))
        {
            throw new ArgumentException(
                "Имя учителя не может быть пустым", 
                nameof(teacher)
            );
        }

        Name = name;
        Teacher = teacher;
        StudentCount = InitialStudentCount;
    }

    public void EnrollStudent()
    {
        StudentCount++;
    }

    public override string ToString()
    {
        return $"{Name} (Учитель: {Teacher}, Студенты: {StudentCount})";
    }
}