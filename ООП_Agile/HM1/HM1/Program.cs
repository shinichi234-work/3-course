namespace HM1;

public class Program
{
    public static void Main()
    {
        var mathematics = new Subject("Математика", "Никита Дмитриевич");
        
        Console.WriteLine(mathematics);
        
        mathematics.EnrollStudent();
        mathematics.EnrollStudent();
        mathematics.EnrollStudent();
        
        Console.WriteLine(mathematics);
    }
}